namespace Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DBContext : DbContext
    {
        public DBContext()
            : base("name=Miao")
        {
        }
        #region 创建DBContext
        /// <summary>
        /// 创建DBContext
        /// </summary>
        /// <returns></returns>
        public static DBContext CreateContext()
        {
            return new DBContext();
        }
        #endregion

        public virtual DbSet<QQUser_info> QQUser_info { get; set; }
        public virtual DbSet<Sys_Area> Sys_Area { get; set; }
        public virtual DbSet<Sys_DbBackup> Sys_DbBackup { get; set; }
        public virtual DbSet<Sys_FilterIP> Sys_FilterIP { get; set; }
        public virtual DbSet<Sys_Items> Sys_Items { get; set; }
        public virtual DbSet<Sys_ItemsDetail> Sys_ItemsDetail { get; set; }
        public virtual DbSet<Sys_Log> Sys_Log { get; set; }
        public virtual DbSet<Sys_Module> Sys_Module { get; set; }
        public virtual DbSet<Sys_ModuleButton> Sys_ModuleButton { get; set; }
        public virtual DbSet<Sys_ModuleForm> Sys_ModuleForm { get; set; }
        public virtual DbSet<Sys_ModuleFormInstance> Sys_ModuleFormInstance { get; set; }
        public virtual DbSet<Sys_Organize> Sys_Organize { get; set; }
        public virtual DbSet<Sys_Role> Sys_Role { get; set; }
        public virtual DbSet<Sys_RoleAuthorize> Sys_RoleAuthorize { get; set; }
        public virtual DbSet<Sys_User> Sys_User { get; set; }
        public virtual DbSet<sys_User_Auths> sys_User_Auths { get; set; }
        public virtual DbSet<Sys_UserLogOn> Sys_UserLogOn { get; set; }
        public virtual DbSet<User_info> User_info { get; set; }
        public virtual DbSet<User_Organize> User_Organize { get; set; }
        public virtual DbSet<User_OrganizeMap> User_OrganizeMap { get; set; }
        public virtual DbSet<User_Type> User_Type { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sys_Area>()
                .Property(e => e.F_Id)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Area>()
                .Property(e => e.F_ParentId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Area>()
                .Property(e => e.F_EnCode)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Area>()
                .Property(e => e.F_FullName)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Area>()
                .Property(e => e.F_SimpleSpelling)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Area>()
                .Property(e => e.F_Description)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Area>()
                .Property(e => e.F_CreatorUserId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Area>()
                .Property(e => e.F_LastModifyUserId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Area>()
                .Property(e => e.F_DeleteUserId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_DbBackup>()
                .Property(e => e.F_Id)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_DbBackup>()
                .Property(e => e.F_BackupType)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_DbBackup>()
                .Property(e => e.F_DbName)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_DbBackup>()
                .Property(e => e.F_FileName)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_DbBackup>()
                .Property(e => e.F_FileSize)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_DbBackup>()
                .Property(e => e.F_FilePath)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_DbBackup>()
                .Property(e => e.F_Description)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_DbBackup>()
                .Property(e => e.F_CreatorUserId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_DbBackup>()
                .Property(e => e.F_LastModifyUserId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_DbBackup>()
                .Property(e => e.F_DeleteUserId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_FilterIP>()
                .Property(e => e.F_Id)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_FilterIP>()
                .Property(e => e.F_StartIP)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_FilterIP>()
                .Property(e => e.F_EndIP)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_FilterIP>()
                .Property(e => e.F_Description)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_FilterIP>()
                .Property(e => e.F_CreatorUserId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_FilterIP>()
                .Property(e => e.F_LastModifyUserId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_FilterIP>()
                .Property(e => e.F_DeleteUserId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Items>()
                .Property(e => e.F_Id)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Items>()
                .Property(e => e.F_ParentId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Items>()
                .Property(e => e.F_EnCode)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Items>()
                .Property(e => e.F_FullName)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Items>()
                .Property(e => e.F_Description)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Items>()
                .Property(e => e.F_CreatorUserId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Items>()
                .Property(e => e.F_LastModifyUserId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Items>()
                .Property(e => e.F_DeleteUserId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_ItemsDetail>()
                .Property(e => e.F_Id)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_ItemsDetail>()
                .Property(e => e.F_ItemId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_ItemsDetail>()
                .Property(e => e.F_ParentId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_ItemsDetail>()
                .Property(e => e.F_ItemCode)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_ItemsDetail>()
                .Property(e => e.F_ItemName)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_ItemsDetail>()
                .Property(e => e.F_SimpleSpelling)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_ItemsDetail>()
                .Property(e => e.F_Description)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_ItemsDetail>()
                .Property(e => e.F_CreatorUserId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_ItemsDetail>()
                .Property(e => e.F_LastModifyUserId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_ItemsDetail>()
                .Property(e => e.F_DeleteUserId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Log>()
                .Property(e => e.F_Id)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Log>()
                .Property(e => e.F_Account)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Log>()
                .Property(e => e.F_NickName)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Log>()
                .Property(e => e.F_Type)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Log>()
                .Property(e => e.F_IPAddress)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Log>()
                .Property(e => e.F_IPAddressName)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Log>()
                .Property(e => e.F_ModuleId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Log>()
                .Property(e => e.F_ModuleName)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Log>()
                .Property(e => e.F_Description)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Log>()
                .Property(e => e.F_CreatorUserId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Module>()
                .Property(e => e.F_Id)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Module>()
                .Property(e => e.F_ParentId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Module>()
                .Property(e => e.F_EnCode)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Module>()
                .Property(e => e.F_FullName)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Module>()
                .Property(e => e.F_Icon)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Module>()
                .Property(e => e.F_UrlAddress)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Module>()
                .Property(e => e.F_Target)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Module>()
                .Property(e => e.F_Description)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Module>()
                .Property(e => e.F_CreatorUserId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Module>()
                .Property(e => e.F_LastModifyUserId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Module>()
                .Property(e => e.F_DeleteUserId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_ModuleButton>()
                .Property(e => e.F_Id)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_ModuleButton>()
                .Property(e => e.F_ModuleId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_ModuleButton>()
                .Property(e => e.F_ParentId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_ModuleButton>()
                .Property(e => e.F_EnCode)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_ModuleButton>()
                .Property(e => e.F_FullName)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_ModuleButton>()
                .Property(e => e.F_Icon)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_ModuleButton>()
                .Property(e => e.F_JsEvent)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_ModuleButton>()
                .Property(e => e.F_UrlAddress)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_ModuleButton>()
                .Property(e => e.F_Description)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_ModuleButton>()
                .Property(e => e.F_CreatorUserId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_ModuleButton>()
                .Property(e => e.F_LastModifyUserId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_ModuleButton>()
                .Property(e => e.F_DeleteUserId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_ModuleForm>()
                .Property(e => e.F_Id)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_ModuleForm>()
                .Property(e => e.F_ModuleId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_ModuleForm>()
                .Property(e => e.F_EnCode)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_ModuleForm>()
                .Property(e => e.F_FullName)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_ModuleForm>()
                .Property(e => e.F_FormJson)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_ModuleForm>()
                .Property(e => e.F_Description)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_ModuleForm>()
                .Property(e => e.F_CreatorUserId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_ModuleForm>()
                .Property(e => e.F_LastModifyUserId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_ModuleForm>()
                .Property(e => e.F_DeleteUserId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_ModuleFormInstance>()
                .Property(e => e.F_Id)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_ModuleFormInstance>()
                .Property(e => e.F_FormId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_ModuleFormInstance>()
                .Property(e => e.F_ObjectId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_ModuleFormInstance>()
                .Property(e => e.F_InstanceJson)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_ModuleFormInstance>()
                .Property(e => e.F_CreatorUserId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Organize>()
                .Property(e => e.F_Id)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Organize>()
                .Property(e => e.F_ParentId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Organize>()
                .Property(e => e.F_EnCode)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Organize>()
                .Property(e => e.F_FullName)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Organize>()
                .Property(e => e.F_ShortName)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Organize>()
                .Property(e => e.F_CategoryId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Organize>()
                .Property(e => e.F_ManagerId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Organize>()
                .Property(e => e.F_TelePhone)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Organize>()
                .Property(e => e.F_MobilePhone)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Organize>()
                .Property(e => e.F_WeChat)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Organize>()
                .Property(e => e.F_Fax)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Organize>()
                .Property(e => e.F_Email)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Organize>()
                .Property(e => e.F_AreaId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Organize>()
                .Property(e => e.F_Address)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Organize>()
                .Property(e => e.F_Description)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Organize>()
                .Property(e => e.F_CreatorUserId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Organize>()
                .Property(e => e.F_LastModifyUserId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Organize>()
                .Property(e => e.F_DeleteUserId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Role>()
                .Property(e => e.F_Id)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Role>()
                .Property(e => e.F_OrganizeId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Role>()
                .Property(e => e.F_EnCode)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Role>()
                .Property(e => e.F_FullName)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Role>()
                .Property(e => e.F_Type)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Role>()
                .Property(e => e.F_Description)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Role>()
                .Property(e => e.F_CreatorUserId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Role>()
                .Property(e => e.F_LastModifyUserId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_Role>()
                .Property(e => e.F_DeleteUserId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_RoleAuthorize>()
                .Property(e => e.F_Id)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_RoleAuthorize>()
                .Property(e => e.F_ItemId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_RoleAuthorize>()
                .Property(e => e.F_ObjectId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_RoleAuthorize>()
                .Property(e => e.F_CreatorUserId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_User>()
                .Property(e => e.F_Id)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_User>()
                .Property(e => e.F_Account)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_User>()
                .Property(e => e.F_RealName)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_User>()
                .Property(e => e.F_NickName)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_User>()
                .Property(e => e.F_HeadIcon)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_User>()
                .Property(e => e.F_MobilePhone)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_User>()
                .Property(e => e.F_Email)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_User>()
                .Property(e => e.F_WeChat)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_User>()
                .Property(e => e.F_ManagerId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_User>()
                .Property(e => e.F_Signature)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_User>()
                .Property(e => e.F_OrganizeId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_User>()
                .Property(e => e.F_DepartmentId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_User>()
                .Property(e => e.F_RoleId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_User>()
                .Property(e => e.F_DutyId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_User>()
                .Property(e => e.F_Description)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_User>()
                .Property(e => e.F_CreatorUserId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_User>()
                .Property(e => e.F_LastModifyUserId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_User>()
                .Property(e => e.F_DeleteUserId)
                .IsUnicode(false);

            modelBuilder.Entity<sys_User_Auths>()
                .Property(e => e.IDENTITYTYPE)
                .IsUnicode(false);

            modelBuilder.Entity<sys_User_Auths>()
                .Property(e => e.IDENTIFIER)
                .IsUnicode(false);

            modelBuilder.Entity<sys_User_Auths>()
                .Property(e => e.CREDENTIAL)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_UserLogOn>()
                .Property(e => e.F_Id)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_UserLogOn>()
                .Property(e => e.F_UserId)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_UserLogOn>()
                .Property(e => e.F_UserPassword)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_UserLogOn>()
                .Property(e => e.F_UserSecretkey)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_UserLogOn>()
                .Property(e => e.F_Question)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_UserLogOn>()
                .Property(e => e.F_AnswerQuestion)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_UserLogOn>()
                .Property(e => e.F_Language)
                .IsUnicode(false);

            modelBuilder.Entity<Sys_UserLogOn>()
                .Property(e => e.F_Theme)
                .IsUnicode(false);

            modelBuilder.Entity<User_OrganizeMap>()
                .HasOptional(e => e.User_OrganizeMap1)
                .WithRequired(e => e.User_OrganizeMap2);

            modelBuilder.Entity<User_OrganizeMap>()
                .HasOptional(e => e.User_OrganizeMap11)
                .WithRequired(e => e.User_OrganizeMap3);
        }
    }
}
