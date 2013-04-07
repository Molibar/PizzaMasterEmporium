using System;
using System.Runtime.Caching;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using PizzaMasterEmporium.Framework.Logging;
using CacheItemPriority = Microsoft.Practices.EnterpriseLibrary.Caching.CacheItemPriority;

namespace PizzaMasterEmporium.Framework.Caching
{
    public interface ICache
    {
        void Add<T>(CacheArguments cacheArguments, T value)
            where T : class;
        T Get<T>(CacheArguments cacheArguments)
            where T : class;
        T Get<T>(CacheArguments cacheArguments, Func<T> func)
            where T : class;
        void Remove(string key);
    }

    /// <summary>
    /// Default minutes to cache is set in the constructor to 15 minutes in
    /// the CacheArgument constructor. Setting the CacheMinutes property in
    /// the CacheArgument overrides default.
    /// </summary>
    [Serializable]
    public class Cache : CacheBase, ICache
    {
        public static bool DisabledDueToTesting;
        private const string CacheInvalidatorSessionKey = "CacheStateManager";

        [NonSerialized]
        private ICacheManager _cacheManager;
        
        public Cache(ILogger logger) : base(logger)
        {
        }

        protected override void InitializeCache()
        {
            if (DisabledDueToTesting) return;
            _cacheManager = CacheFactory.GetCacheManager("WebCacheManager");
        }

        protected override T GetValueFromCacheKey<T>(string key)
        {
            if (DisabledDueToTesting) return null;
            if (IsCacheStale(key))
            {
                return null;
            }

            return (T)_cacheManager.GetData(key);
        }

        protected override void AddToCache<T>(string key, T value, CacheItemPriority normal, object o,
                                           TimeSpan timeSpan)
        {
            if (DisabledDueToTesting) return;

            _cacheManager.Add(key, value, CacheItemPriority.Normal, null,
                              new AbsoluteTime(timeSpan));

            if (IsCacheStale(key))
            {
                ClearStaleFlag(key);
            }
        }

        protected override void RemoveFromCache(string key)
        {
            var stateManager = GetStateManager();
            stateManager.InvalidateCache(key);
        }

        private static CacheStateManager GetStateManager()
        {
            var memoryCache = MemoryCache.Default;

            var cacheInvalidator = (CacheStateManager) memoryCache.Get(CacheInvalidatorSessionKey);
            if (cacheInvalidator == null)
            {
                cacheInvalidator = new CacheStateManager();
                memoryCache.Add(CacheInvalidatorSessionKey, cacheInvalidator,
                                new CacheItemPolicy()
                                    {
                                        AbsoluteExpiration = DateTime.Now.AddSeconds(600)
                                    });
            }
            return cacheInvalidator;
        }

        private static bool IsCacheStale(string key)
        {
            var stateManager = GetStateManager();
            var machineName = Environment.MachineName;

            return stateManager.IsCacheStale(key, machineName);
        }

        private static void ClearStaleFlag(string key)
        {
            var stateManager = GetStateManager();
            var machineName = Environment.MachineName;

            stateManager.ClearStaleFlag(key, machineName);
        }
    }
}