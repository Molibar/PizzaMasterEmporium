using PizzaMasterEmporium.Framework.Logging;

namespace PizzaMasterEmporium.Framework
{
    public class FrameworkBootstrap
    {
        public static void SetUp()
        {
            SetUp("Anonymous App");
        }

        public static void SetUp(string applicationName)
        {
            Log.ApplicationName = applicationName;
            log4net.Config.XmlConfigurator.Configure();
        }
    }
}
