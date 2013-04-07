using System;
using PizzaMasterEmporium.Framework.Logging;
using CacheItemPriority = Microsoft.Practices.EnterpriseLibrary.Caching.CacheItemPriority;

namespace PizzaMasterEmporium.Framework.Caching
{
    [Serializable]
    public abstract class CacheBase
    {
        private ILogger _logger;

        protected CacheBase(ILogger logger)
        {
            InitializeCache();
            _logger = logger;
        }


        /// <summary>
        /// The method that wraps the decorated method.
        /// This method adds the value to the cache.
        /// </summary>
        /// <param name="cacheArguments"></param>
        /// <param name="value">The value to be cached.</param>
        public void Add<T>(CacheArguments cacheArguments, T value)
            where T : class
        {
            AddToCache(cacheArguments.Key, value, CacheItemPriority.Normal, null,
                       new TimeSpan(0, cacheArguments.CacheMinutes, 0));
        }

        /// <summary>
        /// The method that wraps the decorated method.
        /// This method gets a value from the cache.
        /// </summary>
        /// <param name="cacheArguments"></param>
        public T Get<T>(CacheArguments cacheArguments)
            where T : class
        {
            var value = GetValueFromCacheKey<T>(cacheArguments.Key);
            return value;
        }

        /// <summary>
        /// The method that wraps the decorated method.
        /// This method get's the cached value if it is cached, otherwise
        /// calls the function sent in and puts the returned value in the
        /// cache.
        /// </summary>
        /// <param name="cacheArguments"></param>
        /// <param name="func">A function returning the value to be cached.</param>
        public T Get<T>(CacheArguments cacheArguments, Func<T> func)
            where T : class
        {
            var value = GetValueFromCacheKey<T>(cacheArguments.Key);
            if (value != null)
            {
                _logger.LogInfoMessage(GetType(), null, string.Format("Hit cache for key ({0})", cacheArguments.Key));
                return value;
            }
            value = func();
            AddToCache(cacheArguments.Key, value, CacheItemPriority.Normal, null,
                       new TimeSpan(0, cacheArguments.CacheMinutes, 0));
            _logger.LogInfoMessage(GetType(), null, string.Format("Added to cache for key ({0})", cacheArguments.Key));
            return value;
        }

        /// <summary>
        /// Will remove a cached value. 
        /// </summary>
        /// <param name="key">The Cache Key</param>
        public void Remove(string key)
        {
            RemoveFromCache(key);
        }

        protected abstract void InitializeCache();
        protected abstract void AddToCache<T>(string key, T value, CacheItemPriority normal, object o, TimeSpan timeSpan) where T : class;
        protected abstract T GetValueFromCacheKey<T>(string key) where T : class;
        protected abstract void RemoveFromCache(string key);
    }
}