using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using BussinesFacade.Defenitions;
using BussinesFacade.Interfaces;
using BussinesFacade.Models;
using Newtonsoft.Json;


namespace BussinesFacade
{
    public class CoinMarketCapService : ICoinMarketCapService
    {
        private readonly IAppSettings _settings;
        private const string SortField = "marcet_cap";
        private const string SortDirection = "desc";

        public CoinMarketCapService(IAppSettings settings)
        {
            _settings = settings;
        }

        public CurrencyModel GetCurrencies(int limit, string convertCurrency, int startPosition)
        {
            var url = new UriBuilder(_settings.GetSettings("UrlCryptoCurrencyList"));

            var queryString = HttpUtility.ParseQueryString(string.Empty);

            queryString["sort"] = _settings.GetSettings("SortField", SortField);
            queryString["sort_dir"] = _settings.GetSettings("SortDirrection", SortDirection);

            queryString["start"] = startPosition.ToString();
            queryString["limit"] = limit.ToString();
            queryString["convert"] = convertCurrency;

            url.Query = queryString.ToString();

            var client = new WebClient();
            client.Headers.Add("X-CMC_PRO_API_KEY", _settings.GetSettings("APIKey"));
            client.Headers.Add("Accepts", "application/json");

            var json = string.Empty;
            try
            {
                json = client.DownloadString(url.ToString());
            }
            catch (Exception error)
            {
                var status = string.IsNullOrEmpty(error.Message)
                    ? JsonConvert.DeserializeObject<CurrencyModel>(json).Status
                    : ParseError(error.Message);
            }


            var currencies = new CurrencyModel
            {
                Status = status,
                Data = status.ErrorCode == 0
                    ? JsonConvert.DeserializeObject<CurrencyModel>(json).Data
                    : null
            };
            if (currencies.Data == null) return currencies;

            var logoUrlList = GetLogoUrl(currencies.Data.Select(x => x.Id).ToList());
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

        private Dictionary<string, CurrencyInfo>.ValueCollection GetLogoUrl(List<long> idList)
        {
            var url = new UriBuilder(_currencyInfopUrl);
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

        private static Status ParseError(string errorMessage)
        {
            var errorCode = new ErrorCodes().ErrorsDictionary;
            foreach (var error in errorCode)
            {
                if (errorMessage.Contains(error.Key.ToString()))
                    return new Status {ErrorCode = error.Key, ErrorMessage = error.Value};
            }

            return new Status {ErrorCode = 404, ErrorMessage = errorCode[404]};
        }
    }
}