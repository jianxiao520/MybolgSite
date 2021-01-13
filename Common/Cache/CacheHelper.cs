using Bolg.Common.Cache;
using Spring.Context;
using Spring.Context.Support;
using Spring.Objects.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolg.Common.Cache
{
    public class CacheHelper
    {
        public static ICacheWriter CacheWriter { get; set; }

        static CacheHelper()
        {
            //new RedisCacheWriter();
            IApplicationContext ctx = ContextRegistry.GetContext();
            CacheWriter = ((IObjectFactory)ctx).GetObject("CacheWriter") as ICacheWriter;
        }
        /// <summary>
        /// 增加一条键值对缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void AddCache(string key, object value)
        {
            CacheWriter.AddCache(key, value);
        }
        /// <summary>
        /// 增加一条含有过期时间的键值对缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="extDate"></param>
        public static void AddCache(string key, object value, DateTime exDate)
        {
            CacheWriter.AddCache(key, value, exDate);
        }
        /// <summary>
        /// 设置缓存的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetCache(string key, object value)
        {
            CacheWriter.SetCache(key, value);
        }
        /// <summary>
        /// 设置缓存的值以及过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="extDate"></param>
        public static void SetCache(string key, object value, DateTime exDate)
        {
            CacheWriter.SetCache(key, value, exDate);
        }
        /// <summary>
        /// 取得键值对缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetCache<T>(string key)
        {
            return CacheWriter.GetCache<T>(key);
        }
        /// <summary>
        /// 删除键
        /// </summary>
        /// <param name="key"></param>
        public static void RemoveCache(string key)
        {
            CacheWriter.RemoveCache(key);
        }

    }
}
