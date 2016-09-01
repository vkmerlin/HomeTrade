using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Business
{
    public enum CacheKeys
    {
        CategoryNames
    }

    public class CacheProvider : ICacheProvider
    {
        private static ObjectCache Cache
        {
            get { return MemoryCache.Default; }
        }

        public IEnumerable<string> Keys
        {
            get { return Cache.Select(kvp => kvp.Key).ToList(); }
        }

        public object Get(string key)
        {
            return Cache[key];
        }

        public void Set(string key, object data, int cacheTime)
        {
            var policy = new CacheItemPolicy
            {
                AbsoluteExpiration = DateTimeOffset.UtcNow.AddMinutes(cacheTime)
            };
            Cache.Add(new CacheItem(key, data), policy);
        }

        public bool IsSet(string key)
        {
            return (Cache[key] != null);
        }

        public void Invalidate(string key = null)
        {
            if (string.IsNullOrEmpty(key))
            {
                var keys = Keys;
                foreach (var cacheKey in keys)
                {
                    Cache.Remove(cacheKey);
                }
            }
            else
            {
                Cache.Remove(key);
            }
        }
    }
}
