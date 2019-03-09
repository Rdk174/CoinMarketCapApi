using System.Configuration;
using BussinesFacade.Interfaces;

namespace BussinesFacade.Settings
{
    public class WebSettings : IAppSettings
    {
        public string GetSettings(string key, string defaultValue)
        {
            return string.IsNullOrEmpty(ConfigurationManager.AppSettings[key])
                ? ConfigurationManager.AppSettings[key]:defaultValue;
        }

        public string GetSettings(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

    }
}