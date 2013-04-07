using System.Collections.Generic;
using PizzaMasterEmporium.Framework.Configuration.Model;

namespace PizzaMasterEmporium.Framework.Configuration
{
    public interface IConfigurationSettingsRepository
    {
        /// <summary>
        /// Will be responsible for supplying the
        /// IConfigurationSettingsProvider with the data to populate the
        /// ConfigurationPropertyHolder implementations.
        /// </summary>
        /// <param name="context">The context for the ConfigurationSetting
        /// (eg. Images, Paths, Common, Whatever)</param>
        /// <returns>Should return a dictionary where the ParameterName to be
        /// populated should be the key and the ConfigurationSetting object
        /// should be the value.</returns>
        IDictionary<string, ConfigurationSetting> GetConfigurationSettingsDictionary(string context);
    }
}
