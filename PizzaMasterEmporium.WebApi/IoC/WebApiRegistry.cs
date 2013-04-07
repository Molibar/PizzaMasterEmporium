using AutoMapper;
using PizzaMasterEmporium.Data.IoC;
using PizzaMasterEmporium.Domain.IoC;
using PizzaMasterEmporium.Framework.Mapper.AutoMapper;
using StructureMap.Configuration.DSL;

namespace PizzaMasterEmporium.WebApi.IoC
{
    public class WebApiRegistry : Registry
    {
        public WebApiRegistry()
        {
            IncludeRegistry<AutomapperRegistry>();
            IncludeRegistry<DataRegistry>();
            IncludeRegistry<DomainRegistry>();

            Scan(scan =>
            {
                scan.AddAllTypesOf<Profile>();
                scan.TheCallingAssembly();
                scan.WithDefaultConventions();
            });
        }
    }
}