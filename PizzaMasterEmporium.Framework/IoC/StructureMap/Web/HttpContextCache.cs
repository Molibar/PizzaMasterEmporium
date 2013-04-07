using System.Web;
using StructureMap.Pipeline;

namespace PizzaMasterEmporium.Framework.IoC.StructureMap.Web
{
    /// <summary>
    /// A cache implementation for StructureMap to get the Request wide
    /// instances from. This is to be used in the Web Context.
    /// </summary>
    public class HttpContextCache
    {
        private MainObjectCache _cache;
        private const string HTTP_CONTEXT_CACHE_KEY = "TotallyMoney.MoneyMatch.Framework.IoC.StructureMap.WcfServices.HttpContextCache";

        public IObjectCache Cache
        {
            get { return _cache; }
        }

        public static HttpContextCache Current
        {
            get
            {
                if (HttpContext.Current == null) return null;
                var webContextCache =
                    (HttpContextCache)HttpContext.Current.Items[HTTP_CONTEXT_CACHE_KEY];

                if (webContextCache == null)
                {
                    webContextCache = new HttpContextCache {_cache = new MainObjectCache()};
                    HttpContext.Current.Items[HTTP_CONTEXT_CACHE_KEY] = webContextCache;
                }
                return webContextCache;
            }
        }
    }
}