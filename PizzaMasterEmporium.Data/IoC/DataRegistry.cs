using AutoMapper;
using StructureMap.Configuration.DSL;

namespace PizzaMasterEmporium.Data.IoC
{
    public class DataRegistry : Registry
    {
        public DataRegistry()
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
