using AutoMapper;
using StructureMap;

namespace PizzaMasterEmporium.WebApi.IoC
{
    public class IoCForWebApi
    {
        public IContainer Initialize()
        {
            ObjectFactory.Configure(cfg => cfg.AddRegistry<WebApiRegistry>());

            var configuration = ObjectFactory.GetInstance<IConfiguration>();
            foreach (var profile in ObjectFactory.GetAllInstances<Profile>())
            {
                configuration.AddProfile(profile);
            }
            return ObjectFactory.Container;
        }
    }
}