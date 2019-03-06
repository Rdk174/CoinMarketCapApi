using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using AutoMapper;
using BussinesFacade;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly CoinMarketCapService _coinMarketCapService;

        public HomeController()
        {
            _coinMarketCapService = new CoinMarketCapService();
        }

        public ActionResult Index(string currency, string sortDir)
        {
            return View(GetCurrencyData(currency, sortDir));
        }

        private List<CurrencyViewModel> GetCurrencyData(string currency, string sortDir = "asc")
        {
            int.TryParse(ConfigurationManager.AppSettings["CryptoCurrencyListLimit"], out var limit);
            int.TryParse(ConfigurationManager.AppSettings["StartPosition"], out var startPosition);
            var converCurrency = ConfigurationManager.AppSettings["ConvertTo"];
            var sortingField = ConfigurationManager.AppSettings["SortBy"];
            var sortDirection = !string.IsNullOrEmpty(sortDir) ? sortDir : ConfigurationManager.AppSettings["SortDirection"];

            var currencies = _coinMarketCapService.GetCurrencies(currency, limit, converCurrency, sortingField, sortDirection, startPosition);

            return Mapper.Map<List<CurrencyViewModel>>(currencies);
        }
    }
}