using System.Configuration;

namespace BussinesFacade
{
    public static class Settings
    {
        public static string CoinMarketCapUrl => GetSettings("UrlCryptoCurrencyList");

        public static string GetSettings(string key)
        {
            return ConfigurationManager.AppSettings[key].ToString();
        }
    }
}