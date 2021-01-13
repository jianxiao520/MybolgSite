using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolg.Common.Cache
{
    public class MemCachedWriter : ICacheWriter
    {

        //未实现
        public void AddCache(string key, object value)
        {
            throw new NotImplementedException();
        }

        public void AddCache(string key, object value, DateTime extDate)
        {
            throw new NotImplementedException();
        }

        public T GetCache<T>(string key)
        {
            throw new NotImplementedException();
        }

        public void RemoveCache(string key)
        {
            throw new NotImplementedException();
        }

        public void SetCache(string key, object value, DateTime extDate)
        {
            throw new NotImplementedException();
        }

        public void SetCache(string key, object value)
        {
            throw new NotImplementedException();
        }


    }
}
