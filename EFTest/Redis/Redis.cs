using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTest.Redis
{
   public  class Redis
    {
        /// <summary>
        /// 向Redis写入
        /// </summary>
        /// <param name="key">键值</param>
        /// <param name="value">值</param>
        /// <param name="date">过期日期</param>
        public static void SetString(string key, string value, DateTime? date = null)
        {
            //ConnectionMultiplexer.Connect("Localhost:6379,password=123456"))
            using (var redis = ConnectionMultiplexer.Connect("Localhost"))
            {
                //写入
                var db = redis.GetDatabase();
                db.StringSet("key", "123456");
                //设置过期日期
                if (date != null)
                {
                    DateTime time = DateTime.Now.AddSeconds(20);
                    db.KeyExpire("key", time);
                }
                var result = db.StringGet("key");
            }
        }

        /// <summary>
        /// 读取redis的内容
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetString(string key)
        {
            using (var redis = ConnectionMultiplexer.Connect("Localhost"))
            {
                //读取
                var db = redis.GetDatabase();
                var result = db.StringGet(key);
                return result;
            }
        }
    }
}
