using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unity_Interception.Caching
{
    public interface ICacheProvider
    {
        ICacheRegion GetRegion(string regionName);
    }


    public class RunTimeCacheProvider : ICacheProvider
    {
        public ICacheRegion GetRegion(string regionName)
        {
            return new RunTimeCacheRegion(regionName);
        }
    }

    public class RunTimeCacheRegion : ICacheRegion
    {
        private readonly string _regionName;

        public RunTimeCacheRegion(string regionName)
        {
            _regionName = regionName;
        }

        public TValue GetOrAdd<TValue>(string key, Func<TValue> valueFactory)
        {
            // not for production use.
            string fullKey = _regionName + ":" + key;
            if (!System.Runtime.Caching.MemoryCache.Default.Contains(fullKey))
            {
                var value = valueFactory.Invoke();
                System.Runtime.Caching.MemoryCache.Default.Set(fullKey, value, DateTimeOffset.Now.Add(TimeSpan.FromMinutes(30)));
                return value;
            }

            return (TValue)System.Runtime.Caching.MemoryCache.Default.Get(fullKey);
        }
    }
}
