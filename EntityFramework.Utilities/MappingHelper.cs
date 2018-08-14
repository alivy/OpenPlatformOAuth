/// <summary>
/// Adapted from http://romiller.com/2013/09/24/ef-code-first-mapping-between-types-tables/ 
/// This whole file contains a hack needed because the mapping API is internal pre 6.1 atleast
/// </summary>
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Reflection;

namespace EntityFramework.Utilities
{

    /// <summary>
    /// Represents the mapping of an entitiy type to one or mode tables in the database
    ///
    /// A single entity can be mapped to more than one table when 'Entity Splitting' is used
    /// Entity Splitting involves mapping different properties from the same type to different tables
    /// See http://msdn.com/data/jj591617#2.7 for more details
    /// </summary>
    public class TypeMapping
    {
        /// <summary>
        /// The type of the entity from the model
        /// </summary>
        public Type EntityType { get; set; }

        /// <summary>
        /// The table(s) that the entity is mapped to
        /// </summary>
        public List<TableMapping> TableMappings { get; set; }
    }

    /// <summary>
    ///代表一个实体的映射到数据库中的一个表
    /// </summary>
    public class TableMapping
    {
        /// <summary>
        /// 表的名称映射到的实体
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// 表的模式映射到的实体
        /// </summary>
        public string Schema { get; set; }

        /// <summary>
        /// Details of the property-to-column mapping
        /// </summary>
        public List<PropertyMapping> PropertyMappings { get; set; }

        /// <summary>
        /// Null if not TPH
        /// </summary>
        public TPHConfiguration TPHConfiguration { get; set; }
    }

    public class TPHConfiguration
    {
        public Dictionary<Type, string> Mappings { get; set; }
        public string ColumnName { get; set; }

    }

    /// <summary>
    /// 表示属性到数据库中列的映射
    /// </summary>
    public class PropertyMapping
    {
        /// <summary>
        /// 通往这家酒店的物业链。对于标量属性，这是单个值，但对于复杂属性，这是一个点（。）分隔列表
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// 属性映射到的列
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 当我们有TPH来排除实体时使用
        /// </summary>
        public Type ForEntityType { get; set; }

        public string DataType { get; set; }

        public bool IsPrimaryKey { get; set; }

        public string DataTypeFull { get; set; }
    }

    /// <summary>
    /// Represents that mapping between entity types and tables in an EF model
    /// </summary>
    public class EfMapping
    {
        /// <summary>
        /// 映射模型中每个实体类型的信息
        /// </summary>
        public Dictionary<Type, TypeMapping> TypeMappings { get; set; }

        /// <summary>
        /// 初始化Ef Mapping类的实例
        /// </summary>
        /// <param name="db">The context to get the mapping from</param>
        public EfMapping(DbContext db)
        {
            this.TypeMappings = new Dictionary<Type, TypeMapping>();

            var metadata = ((IObjectContextAdapter)db).ObjectContext.MetadataWorkspace;

            //EF61Test（元数据）;

            // 模型的概念部分包含有关实体类形状的信息
            var conceptualContainer = metadata.GetItems<EntityContainer>(DataSpace.CSpace).Single();

            //模型的存储部分包含有关表格形状的信息
            var storeContainer = metadata.GetItems<EntityContainer>(DataSpace.SSpace).Single();

            // 模型的对象部分，包含有关实际CLR类型的信息
            var objectItemCollection = ((ObjectItemCollection)metadata.GetItemCollection(DataSpace.OSpace));

            // 循环遍历模型中的每个实体类型
            foreach (var set in conceptualContainer.BaseEntitySets.OfType<EntitySet>())
            {

                //查找此实体集的概念模型和存储模型之间的映射
                var mapping = metadata.GetItems<EntityContainerMapping>(DataSpace.CSSpace)
                        .Single()
                        .EntitySetMappings
                        .Single(s => s.EntitySet == set);



                var typeMapping = new TypeMapping
                {
                    TableMappings = new List<TableMapping>(),
                    EntityType = GetClrType(metadata, objectItemCollection, set)
                };

                this.TypeMappings.Add(typeMapping.EntityType, typeMapping);

                var tableMapping = new TableMapping
                {
                    PropertyMappings = new List<PropertyMapping>(),
                };
                var mappingToLookAt = mapping.EntityTypeMappings.FirstOrDefault(m => m.IsHierarchyMapping) ?? mapping.EntityTypeMappings.First();
                tableMapping.Schema = mappingToLookAt.Fragments[0].StoreEntitySet.Schema;
                tableMapping.TableName = mappingToLookAt.Fragments[0].StoreEntitySet.Table ?? mappingToLookAt.Fragments[0].StoreEntitySet.Name;
                typeMapping.TableMappings.Add(tableMapping);

#pragma warning disable IDE0039 // 使用本地函数
                Action<Type, System.Data.Entity.Core.Mapping.PropertyMapping, string> recurse = null;
#pragma warning restore IDE0039 // 使用本地函数
                recurse = (t, item, path) =>
                {
                    if (item is ComplexPropertyMapping)
                    {
                        var complex = item as ComplexPropertyMapping;
                        foreach (var child in complex.TypeMappings[0].PropertyMappings)
                        {
                            recurse(t, child, path + complex.Property.Name + ".");
                        }
                    }
                    else if (item is ScalarPropertyMapping)
                    {
                        var scalar = item as ScalarPropertyMapping;
                        tableMapping.PropertyMappings.Add(new PropertyMapping
                        {
                            ColumnName = scalar.Column.Name,
                            DataType = scalar.Column.TypeName,
                            DataTypeFull = GetFullTypeName(scalar),
                            PropertyName = path + item.Property.Name,
                            ForEntityType = t
                        });
                    }
                };

#pragma warning disable IDE0039 // 使用本地函数
                Func<MappingFragment, Type> getClr = m =>
#pragma warning restore IDE0039 // 使用本地函数
                {
                    return GetClrTypeFromTypeMapping(metadata, objectItemCollection, m.TypeMapping as EntityTypeMapping);
                };

                if (mapping.EntityTypeMappings.Any(m => m.IsHierarchyMapping))
                {
                    var withConditions = mapping.EntityTypeMappings.Where(m => m.Fragments[0].Conditions.Any()).ToList();
                    tableMapping.TPHConfiguration = new TPHConfiguration
                    {
                        ColumnName = withConditions.First().Fragments[0].Conditions[0].Column.Name,
                        Mappings = new Dictionary<Type, string>()
                    };
                    foreach (var item in withConditions)
                    {
                        tableMapping.TPHConfiguration.Mappings.Add(
                            getClr(item.Fragments[0]),
                            ((ValueConditionMapping)item.Fragments[0].Conditions[0]).Value.ToString()
                            );
                    }
                }

                foreach (var entityType in mapping.EntityTypeMappings)
                {
                    foreach (var item in entityType.Fragments[0].PropertyMappings)
                    {
                        recurse(getClr(entityType.Fragments[0]), item, "");
                    }
                }

                //Inheriting propertymappings contains duplicates for id's. 
                tableMapping.PropertyMappings = tableMapping.PropertyMappings.GroupBy(p => p.PropertyName)
                    .Select(g => g.OrderByDescending(outer => g.Count(inner => inner.ForEntityType.IsSubclassOf(outer.ForEntityType))).First())
                    .ToList();
                foreach (var item in tableMapping.PropertyMappings)
                {
                    if ((mappingToLookAt.EntityType ?? mappingToLookAt.IsOfEntityTypes[0]).KeyProperties.Any(p => p.Name == item.PropertyName))
                    {
                        item.IsPrimaryKey = true;
                    }
                }
            }
        }

        private string GetFullTypeName(ScalarPropertyMapping scalar)
        {
            if (scalar.Column.TypeName == "nvarchar" || scalar.Column.TypeName == "varchar")
            {
                return string.Format("{0}({1})", scalar.Column.TypeName, scalar.Column.MaxLength);
            }

            if (scalar.Column.TypeName == "decimal" || scalar.Column.TypeName == "numeric")
            {
                return string.Format("{0}({1},{2})", scalar.Column.TypeName, scalar.Column.Precision, scalar.Column.Scale);
            }

            return scalar.Column.TypeName;
        }

        private Type GetClrTypeFromTypeMapping(MetadataWorkspace metadata, ObjectItemCollection objectItemCollection, EntityTypeMapping mapping)
        {
            return GetClrType(metadata, objectItemCollection, mapping.EntityType ?? mapping.IsOfEntityTypes.First());
        }

        private static dynamic GetProperty(string property, object instance)
        {
            var type = instance.GetType();
            return type.InvokeMember(property, BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance, null, instance, null);
        }

        private string GetFullName(XElement propertyMapping)
        {
            if (propertyMapping.Parent.Name.LocalName == "ComplexProperty")
            {
                return GetFullName(propertyMapping.Parent) + "." + propertyMapping.Attribute("Name").Value;
            }
            return propertyMapping.Attribute("Name").Value;
        }

        private static Type GetClrType(MetadataWorkspace metadata, ObjectItemCollection objectItemCollection, EntitySet set)
        {
            return GetClrType(metadata, objectItemCollection, set.ElementType);
        }

        private static Type GetClrType(MetadataWorkspace metadata, ObjectItemCollection objectItemCollection, EntityTypeBase type)
        {
            return metadata
                   .GetItems<EntityType>(DataSpace.OSpace)
                   .Select(objectItemCollection.GetClrType)
                   .Single(e => e.Name == type.Name);
        }

        private static XDocument GetEdmx(DbContext db)
        {
            XDocument doc;
            using (var memoryStream = new MemoryStream())
            {
                using (var xmlWriter = XmlWriter.Create(
                    memoryStream, new XmlWriterSettings
                    {
                        Indent = true
                    }))
                {
                    EdmxWriter.WriteEdmx(db, xmlWriter);
                }

                memoryStream.Position = 0;

                doc = XDocument.Load(memoryStream);
            }
            return doc;
        }
    }

    public static class EfMappingFactory
    {
        private static Dictionary<Type, EfMapping> cache = new Dictionary<Type, EfMapping>();

        public static EfMapping GetMappingsForContext(DbContext context)
        {
            var type = context.GetType();
            if (!cache.TryGetValue(type, out EfMapping mapping))
            {
                mapping = new EfMapping(context);
                cache.Add(type, mapping);
            }
            return mapping;
        }

    }
}
