using System.Configuration;
using System.Linq;
using BussinesFacade.Interfaces;

namespace BussinesFacade.Settings
{
    public class WebSettings : ISettings
    {
        public string GetSettings(string key, string defaultValue)
        {
            if (!IsValidKey(key))
            {
                return defaultValue;
            }

            return !string.IsNullOrEmpty(ConfigurationManager.AppSettings[key])
                ? ConfigurationManager.AppSettings[key]
                : defaultValue;
        }

        public string GetSettings(string key)
        {
            if (!IsValidKey(key))
            {
                return string.Empty;
            }

            return ConfigurationManager.AppSettings[key];
        }

        private bool IsValidKey(string key)
        {
            return ConfigurationManager.AppSettings.AllKeys.Contains(key);
        }
    }
}