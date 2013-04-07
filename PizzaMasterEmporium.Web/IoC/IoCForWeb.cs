using AutoMapper;
using StructureMap;

namespace PizzaMasterEmporium.Web.IoC
{
    public class IoCForWeb
    {
        public IContainer Initialize()
        {
            ObjectFactory.Configure(cfg => cfg.AddRegistry<WebRegistry>());

            var configuration = ObjectFactory.GetInstance<IConfiguration>();
            foreach (var profile in ObjectFactory.GetAllInstances<Profile>())
            {
                configuration.AddProfile(profile);
            }
            return ObjectFactory.Container;
        }
    }
}