using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace PizzaMasterEmporium.Framework.IoC.StructureMap.WcfServices
{
    /// <summary>
    /// http://andreasohlund.net/2009/04/27/unitofwork-in-wcf-using-structuremap/
    /// 
    /// Required for the WCF Context to be able to set up StructureMap
    /// instance caching.
    /// </summary>
    public class InstanceContextCacheInitializer : IInstanceContextInitializer
    {
        public void Initialize(InstanceContext instanceContext, Message message)
        {
            instanceContext.Extensions.Add(new OperationContextCache());
        }
    }
}