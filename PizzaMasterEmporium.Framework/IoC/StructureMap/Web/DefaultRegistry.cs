using StructureMap.Configuration.DSL;

namespace PizzaMasterEmporium.Framework.IoC.StructureMap.Web
{
    public class DefaultRegistry : Registry
    {
        public DefaultRegistry()
        {
            Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.AssembliesFromApplicationBaseDirectory();
                scan.WithDefaultConventions();
            });
        }
    }
}
