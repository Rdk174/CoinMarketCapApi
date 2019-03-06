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

        public ActionResult Index()
        {
            int.TryParse(ConfigurationManager.AppSettings["CryptoCurrencyListLimit"], out var limit);
            int.TryParse(ConfigurationManager.AppSettings["StartPosition"], out var startPosition);
            var converCurrency = ConfigurationManager.AppSettings["ConvertTo"];
            var sortingField = ConfigurationManager.AppSettings["SortBy"];
            var sortDirection = ConfigurationManager.AppSettings["SortDirection"];
            var currencies = _coinMarketCapService.GetCurrencies(limit, converCurrency, sortingField, sortDirection, startPosition);

            var model = Mapper.Map<List<CurrencyViewModel>>(currencies);

            return View(model);
        }
    }
}