using System;
using System.ServiceModel;

namespace PizzaMasterEmporium.Framework.IoC.StructureMap.WcfServices
{
    public class StructureMapServiceHost : ServiceHost
    {
        public StructureMapServiceHost()
        {
        }

        public StructureMapServiceHost(Type serviceType, params Uri[] baseAddresses)
            : base(serviceType, baseAddresses)
        {
        }

        protected override void OnOpening()
        {
            Description.Behaviors.Add(new StructureMapServiceBehavior());
            base.OnOpening();
        }
    }
}
