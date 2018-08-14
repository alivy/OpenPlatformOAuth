using EntityFramework.Utilities;
using Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EFTest.Batch
{
    public class Batch
    {
       
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<User_info> GetInsertDatas()
        {
            // 线程安全的list
            ConcurrentBag<User_info> datas = new ConcurrentBag<User_info>();
            Parallel.For(0, 100000, (index, state) =>
            {
                Random rand = new Random();
                var newData = new User_info { UserID = rand.Next(1, 100).ToString(), User_Name = "batch", User_Pwd = "密码" + index.ToString() };
                datas.Add(newData);
            });

            return datas;
        }
               /// <summary>
        ///     批量插入
        /// </summary>
        public static void BatchInster()
        {
            var datas = GetInsertDatas();
            var testEntities = datas as IList<User_info> ?? datas.ToList();

            Stopwatch watch = new Stopwatch();

            Console.WriteLine("开始插入计时,总共数据:{0}条", testEntities.Count());
            watch.Start();

            using (var context = DBContext.CreateContext())
            {
                EFBatchOperation.For(context, context.User_info).InsertAll(testEntities);
            }

            watch.Stop();
            Console.WriteLine("结束插入计时,工用时:{0}ms", watch.ElapsedMilliseconds);


            using (var context = DBContext.CreateContext())
            {
                var count = context.User_info.Count();
                Console.WriteLine("数据库总共数据:{0}条", count);

                var minId = context.User_info.Min(c => c.Id);

                // 随机取十条数据进行验证

                for (int i = 1; i <= 10; i++)
                {
                    Random rand = new Random();
                    var id = rand.Next(minId, minId + 100000);

                    var testdata = context.User_info.FirstOrDefault(c => c.Id == id);

                    Console.WriteLine("插入的数据 id:{0} randomvalue:{1}", testdata.Id, testdata.User_Pwd);

                }
            }
            Console.WriteLine("-----------------华丽的分割线   插入-------------------------");
        }



        /// <summary>
        ///     批量更新
        /// </summary>
        public static void BatchUpdate()
        {
            IEnumerable<User_info> toUpdates = new List<User_info>();

            // 获取所有数据
            using (var context = DBContext.CreateContext())
            {
                toUpdates = context.User_info.ToList();
            }

            int Id = 0;
            // 所有的值 都为 1000
            Parallel.ForEach(toUpdates, (entity, state) => { entity.User_Name = "更新用户名" + Id++; });


            Stopwatch watch = new Stopwatch();

            Console.WriteLine("开始更新计时,总共数据:{0}条", toUpdates.Count());
            watch.Start();

            using (var context = DBContext.CreateContext())
            {
                EFBatchOperation.For(context, context.User_info).UpdateAll(toUpdates, x => x.ColumnsToUpdate(c => c.User_Name));
            }

            watch.Stop();
            Console.WriteLine("结束更新计时,工用时:{0}ms", watch.ElapsedMilliseconds);


            using (var context = DBContext.CreateContext())
            {
                var count = context.User_info.Count();
                Console.WriteLine("数据库总共数据:{0}条", count);

                var minId = context.User_info.Min(c => c.Id);

                // 随机取十条数据进行验证

                for (int i = 1; i <= 10; i++)
                {
                    Random Rand = new Random();
                    var id = Rand.Next(minId, minId + 100000);
                    var testdata = context.User_info.FirstOrDefault(c => c.Id == id);
                    Console.WriteLine("更新的数据 id:{0} randomvalue:{1}", testdata.Id, testdata.User_Name);

                }

            }
            Console.WriteLine("-----------------华丽的分割线   更新-------------------------");
        }

        /// <summary>
        ///     将id >= 1w  小于 5w 的随机值等于 500
        /// </summary>
        private static void BatchUpdateQuery()
        {

            Stopwatch watch = new Stopwatch();

            Console.WriteLine("开始查询更新计时");
            watch.Start();

            using (var context = DBContext.CreateContext())
            {

                var minId = context.User_info.Min(c => c.Id);

                EFBatchOperation.For(context, context.User_info)
                    .Where(c => c.Id >= minId + 10000 && c.Id <= minId + 50000)
                    .Update(c => c.Remark, xy => "");


            }

            watch.Stop();
            Console.WriteLine("结束查询更新计时,工用时:{0}ms", watch.ElapsedMilliseconds);

            using (var context = DBContext.CreateContext())
            {
                var count = context.User_info.Count();
                Console.WriteLine("数据库总共数据:{0}条", count);
                var minId = context.User_info.Min(c => c.Id);
                // 随机取十条数据进行验证
                for (int i = 1; i <= 10; i++)
                {
                    Random rand = new Random();
                    var id = rand.Next(minId + 10000, minId + 50000);

                    var testdata = context.User_info.FirstOrDefault(c => c.Id == id);
                    Console.WriteLine("查询更新的数据 id:{0} randomvalue:{1}", testdata.Id, testdata.Id);

                }

            }
            Console.WriteLine("-----------------华丽的分割线  查询更新-------------------------");
        }



        /// <summary>
        ///     删除所有数据
        /// </summary>
        private static void BatchDelete()
        {

            Stopwatch watch = new Stopwatch();

            Console.WriteLine("开始删除计时");
            watch.Start();

            using (var context = DBContext.CreateContext())
            {
                EFBatchOperation.For(context, context.User_info)
                    .Where(c => c.Id >= 1).Delete();
            }

            watch.Stop();
            Console.WriteLine("结束删除计时,工用时:{0}ms", watch.ElapsedMilliseconds);

            using (var context = DBContext.CreateContext())
            {
                var count = context.User_info.Count();
                Console.WriteLine("数据库总共数据:{0}条", count);
            }
            Console.WriteLine("-----------------华丽的分割线  删除-------------------------");

        }


    }
}
