using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolg.Common.Cache
{
    public interface ICacheWriter
    {

        void AddCache(string key, object value);

        void AddCache(string key, object value, DateTime extDate);

        T GetCache<T>(string key);

        void SetCache(string key, object value, DateTime extDate);

        void SetCache(string key, object value);

        void RemoveCache(string key);
    }
}
