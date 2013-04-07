using System;
using System.Configuration;
using PizzaMasterEmporium.Framework.Configuration;
using PizzaMasterEmporium.Framework.Configuration.Repositories;
using PizzaMasterEmporium.Framework.DataAccess;
using StructureMap.Configuration.DSL;

namespace PizzaMasterEmporium.Framework.IoC
{
    public class FrameworkRegistry : Registry
    {
        public FrameworkRegistry()
        {
            For<IDatabaseConnectionStringProvider>()
                .Singleton()
                .Use(x =>
                         {
                             var databaseServer =
                                 ConfigurationManager.AppSettings["ConnectionStringDatabaseServer"];
                             if (string.IsNullOrEmpty(databaseServer))
                                 databaseServer = Environment.MachineName;
                             var connectionString = string.Format(
                                 ConfigurationManager.ConnectionStrings["Main"].
                                     ConnectionString, databaseServer);
                             return
                                 new DatabaseConnectionStringProvider(connectionString);
                         });
            For<IConfigurationSettingsRepository>().Use<MsSqlConfigurationSettingsRepository>();
            For<IFileSerializer>().Use<XmlFileSerializer>();

            Scan(scan =>
                     {
                         scan.TheCallingAssembly();
                         scan.WithDefaultConventions();
                     });
            
        }
    }
}
