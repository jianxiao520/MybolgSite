using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolg.Common.Cache
{
    public class RedisCacheWriter : ICacheWriter
    {
        /// <summary>
        /// redis配置文件信息
        /// </summary>

        private PooledRedisClientManager _prcm;
        private IRedisClient RedisClient;
        /// <summary>
        /// 静态构造方法，初始化链接池管理对象
        /// </summary>
        public RedisCacheWriter()
        {
            RedisClient = GetClient();
        }
        /// <summary>
        /// 创建链接池管理对象
        /// </summary>
        private void CreateManager()
        {
            //string strAppMemCachedServer = ConfigurationManager.AppSettings["MemCachedServerList"];
            string RedisPath = ConfigurationManager.AppSettings["MemCachedServerList"];
            _prcm = CreateManager(new string[] { RedisPath }, new string[] { RedisPath });
        }

        private PooledRedisClientManager CreateManager(string[] readWriteHosts, string[] readOnlyHosts)
        {
            //WriteServerList：可写的Redis链接地址。
            //ReadServerList：可读的Redis链接地址。
            //MaxWritePoolSize：最大写链接数。
            //MaxReadPoolSize：最大读链接数。
            //AutoStart：自动重启。
            //LocalCacheTime：本地缓存到期时间，单位:秒。
            //RecordeLog：是否记录日志,该设置仅用于排查redis运行时出现的问题,如redis工作正常,请关闭该项。
            //RedisConfigInfo类是记录redis连接信息，此信息和配置文件中的RedisConfig相呼应
            // 支持读写分离，均衡负载 
            return new PooledRedisClientManager(readWriteHosts, readOnlyHosts, new RedisClientManagerConfig
            {
                MaxWritePoolSize = 5, // “写”链接池链接数 
                MaxReadPoolSize = 5, // “读”链接池链接数 
                AutoStart = true,
            });
        }

        private IEnumerable<string> SplitString(string strSource, string split)
        {
            return strSource.Split(split.ToArray());
        }
        /// <summary>
        /// 客户端缓存操作对象
        /// </summary>
        public IRedisClient GetClient()
        {
            if (_prcm == null)
            {
                CreateManager();
            }
            return _prcm.GetClient();
        }

        public void AddCache(string key, object value)
        {
            RedisClient.Add(key, value);
        }

        public void AddCache(string key, object value, DateTime extDate)
        {
            RedisClient.Add(key, value, extDate);
        }

        public T GetCache<T>(string key)
        {
            return RedisClient.Get<T>(key);
        } 

        public void SetCache(string key, object value, DateTime extDate)
        {
            RedisClient.Set(key, value, extDate);
        }

        public void SetCache(string key, object value)
        {
            RedisClient.Set(key, value);
        }

        public void RemoveCache(string key)
        {
            RedisClient.Remove(key);
        }
    }
}
