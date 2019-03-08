using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Web;
using BussinesFacade.Defenitions;
using Newtonsoft.Json;


namespace BussinesFacade
{
    public class CoinMarketCapService
    {
        private readonly string _coinMarketCapUrl;
        private readonly string _currencyInfopUrl;
        private readonly string _apiKey;

        public CoinMarketCapService()
        {
            _coinMarketCapUrl = ConfigurationManager.AppSettings["UrlCryptoCurrencyList"];
            _currencyInfopUrl = ConfigurationManager.AppSettings["UrlCryptoCurrencyInfo"];
            _apiKey = ConfigurationManager.AppSettings["APIKey"];
        }

        public CurrencyModel GetCurrencies(int limit, string convertCurrency, string sortingFields,
            string sortDirrections, int startPosition = 1)
        {
            var url = new UriBuilder(_coinMarketCapUrl);

            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["start"] = startPosition.ToString();
            queryString["limit"] = limit.ToString();
            queryString["convert"] = convertCurrency;
            queryString["sort"] = sortingFields;
            queryString["sort_dir"] = sortDirrections;

            url.Query = queryString.ToString();

            var client = new WebClient();
            client.Headers.Add("X-CMC_PRO_API_KEY", _apiKey);
            client.Headers.Add("Accepts", "application/json");
            var json = string.Empty;
            var errorString = string.Empty;
            try
            {
                json = client.DownloadString(url.ToString());
            }
            catch (Exception error)
            {
                errorString = error.ToString();
            }

            var status = string.IsNullOrEmpty(errorString)
                ? JsonConvert.DeserializeObject<CurrencyModel>(json).Status
                : ParseError(errorString);

            var currencies = new CurrencyModel
            {
                Status = status,
                Data = status.ErrorCode == 0
                    ? JsonConvert.DeserializeObject<CurrencyModel>(json).Data
                    : null
            };
            if (currencies.Data == null) return currencies;
            var currenciesIdList = new List<long>();
            foreach (var currency in currencies.Data)
            {
                currenciesIdList.Add(currency.Id);
            }

            var logoUrlList = GetLogoUrl(currenciesIdList);
            if (logoUrlList == null) return currencies;

            foreach (var currency in currencies.Data)
            {
                foreach (var logoUrl in logoUrlList)
                {
                    if (currency.Id == logoUrl.Id) currency.Logo = logoUrl.Logo;
                }
            }

            return currencies;
        }

        public Dictionary<string, CurrencyInfo>.ValueCollection GetLogoUrl(List<long> idList)
        {
            var url = new UriBuilder("https://pro-api.coinmarketcap.com/v1/cryptocurrency/info");
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["id"] = string.Join(",", idList);

            url.Query = queryString.ToString();

            var client = new WebClient();
            client.Headers.Add("X-CMC_PRO_API_KEY", _apiKey);
            client.Headers.Add("Accepts", "application/json");
            try
            {
                var json = client.DownloadString(url.ToString());
                return JsonConvert.DeserializeObject<CurrencyInfoModel>(json).Status.ErrorCode == 0
                    ? JsonConvert.DeserializeObject<CurrencyInfoModel>(json).Data.Values
                    : null;
            }
            catch
            {
                return null;
            }
        }

        private Status ParseError(string errorMessage)
        {
            var errorCode = new ErrorCodes().ErrorsDictionary;
            foreach (var error in errorCode)
            {
                if (errorMessage.Contains(error.Key.ToString()))
                    return new Status {ErrorCode = error.Key, ErrorMessage = error.Value};
            }

            return new Status { ErrorCode = 404, ErrorMessage = errorCode[404] }; ;
        }
    }
}