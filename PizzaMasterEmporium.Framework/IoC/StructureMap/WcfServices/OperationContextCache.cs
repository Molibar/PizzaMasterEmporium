using System.ServiceModel;
using StructureMap.Pipeline;

namespace PizzaMasterEmporium.Framework.IoC.StructureMap.WcfServices
{
    /// <summary>
    /// http://andreasohlund.net/2009/04/27/unitofwork-in-wcf-using-structuremap/
    /// 
    /// A cache implementation for StructureMap to get the Request wide
    /// instances from. This is to be used in the WCF Context.
    /// </summary>
    public class OperationContextCache : IExtension<InstanceContext>
    {
        private MainObjectCache _cache;

        public IObjectCache Cache
        {
            get { return _cache; }
        }

        public static OperationContextCache Current
        {
            get
            {
                if (OperationContext.Current == null) return null;
                return OperationContext.Current.InstanceContext.Extensions.Find<OperationContextCache>();
            }
        }

        public void Attach(InstanceContext owner)
        {
            _cache = new MainObjectCache();
        }

        public void Detach(InstanceContext owner)
        {

        }
    }
}