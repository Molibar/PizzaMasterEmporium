using System.Web;
using PizzaMasterEmporium.Framework.IoC.StructureMap.WcfServices;
using PizzaMasterEmporium.Framework.IoC.StructureMap.Web;
using StructureMap.Pipeline;

namespace PizzaMasterEmporium.Framework.IoC.StructureMap
{
    /// <summary>
    /// http://andreasohlund.net/2009/04/27/unitofwork-in-wcf-using-structuremap/
    /// 
    /// To be able to implement the Unit of Work pattern with StructureMap in
    /// both the OperationContext (WCF Service) AND the HttpContext (Web) this
    /// class is responsible of choosing what type of cache to store the
    /// object instances in.
    /// </summary>
    public class UnitOfWorkLifecycle : ILifecycle
    {
        public void EjectAll()
        {
            FindCache().DisposeAndClear();
        }

        public IObjectCache FindCache()
        {
            return (HttpContext.Current != null)
                       ? HttpContextCache.Current.Cache
                       : OperationContextCache.Current.Cache;
        }

        public string Scope
        {
            get { return GetType().Name; }
        }
    }
}