using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using BussinesFacade.Interfaces;
using BussinesFacade.Models;
using Newtonsoft.Json;


namespace BussinesFacade
{
    public class CoinMarketCapService : ICurrencyService
    {
        private readonly ISettings _settings;
        private readonly ILogger _logger;
        private const string SortField = "market_cap";
        private const string SortDirection = "desc";
        private readonly string _apiKey;

        public CoinMarketCapService(ISettings settings, ILogger logger)
        {
            _settings = settings;
            _logger = logger;
            _apiKey = _settings.GetSettings("APIKey");
        }

        public List<Data> GetCurrencies(int limit, string convertCurrency, int startPosition)
        {
            var urlGetCurrencies = _settings.GetSettings("UrlCryptoCurrencyList");
            if (string.IsNullOrEmpty(urlGetCurrencies))
            {
                _logger.Error("Bad URL for request currencies");
                return null;
            }

            var url = new UriBuilder(urlGetCurrencies);

            var queryString = HttpUtility.ParseQueryString(string.Empty);

            queryString["sort"] = _settings.GetSettings("SortField", SortField);
            queryString["sort_dir"] = _settings.GetSettings("SortDirection", SortDirection);

            queryString["start"] = startPosition.ToString();
            queryString["limit"] = limit.ToString();
            queryString["convert"] = convertCurrency;

            url.Query = queryString.ToString();

            var client = new WebClient();
            client.Headers.Add("X-CMC_PRO_API_KEY", _apiKey);
            client.Headers.Add("Accepts", "application/json");
            List<Data> currencies;
            try
            {
                currencies = JsonConvert.DeserializeObject<CurrencyModel>(client.DownloadString(url.ToString())).Data;
            }
            catch (Exception error)
            {
                _logger.Error(error.Message);
                return null;
            }

            var logoUrlList = GetLogoUrl(currencies.Select(x => x.Id).ToList());
            if (logoUrlList == null)
                return currencies;

            foreach (var currency in currencies)
            {
                currency.Logo = logoUrlList.FirstOrDefault(x => x.Id == currency.Id)?.Logo;
            }

            return currencies;
        }

        private Dictionary<string, CurrencyInfo>.ValueCollection GetLogoUrl(List<long> idList)
        {
            var urlGetCurrenciesInfo = _settings.GetSettings("UrlCryptoCurrencyInfo");
            if (string.IsNullOrEmpty(urlGetCurrenciesInfo))
            {
                _logger.Error("Bad URL for request currencies info");
                return null;
            }

            var url = new UriBuilder(urlGetCurrenciesInfo);
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["id"] = string.Join(",", idList);

            url.Query = queryString.ToString();

            var client = new WebClient();
            client.Headers.Add("X-CMC_PRO_API_KEY", _apiKey);
            client.Headers.Add("Accepts", "application/json");
            try
            {
                return JsonConvert.DeserializeObject<CurrencyInfoModel>(client.DownloadString(url.ToString())).Data
                    .Values;
            }
            catch (Exception error)
            {
                _logger.Error(error.Message);
                return null;
            }
        }
    }
}