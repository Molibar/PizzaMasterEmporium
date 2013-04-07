using AutoMapper;
using StructureMap.Configuration.DSL;

namespace PizzaMasterEmporium.Domain.IoC
{
    public class DomainRegistry : Registry
    {
        public DomainRegistry()
        {
            Scan(scan =>
            {
                scan.AddAllTypesOf<Profile>();
                scan.TheCallingAssembly();
                scan.WithDefaultConventions();
            });
        }
    }
}
