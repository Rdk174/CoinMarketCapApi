using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using AutoMapper;
using BussinesFacade;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private CoinMarketCapService _coinMarketCapService;
        private IndexViewModel _indexViewModel;

        [OutputCache(Duration = 60, Location = OutputCacheLocation.Client)]
        public ActionResult Index(string currency="")
        {
            _coinMarketCapService = new CoinMarketCapService();
            _indexViewModel = new IndexViewModel
            {
                CurrencyView = GetCurrencyData(),
                SelectedFilter = "All"
            };
            if (!string.IsNullOrEmpty(currency))
            {
                _indexViewModel.CurrencyView = new List<CurrencyViewModel>()
                {
                    (_indexViewModel.CurrencyView.FirstOrDefault(x =>
                        string.Equals(x.Name, currency, StringComparison.OrdinalIgnoreCase)))
                };
                _indexViewModel.SelectedFilter = currency;
            }

            return View(_indexViewModel);
        }

        private List<CurrencyViewModel> GetCurrencyData()
        {
            int.TryParse(ConfigurationManager.AppSettings["CryptoCurrencyListLimit"], out var limit);
            int.TryParse(ConfigurationManager.AppSettings["StartPosition"], out var startPosition);
            var converCurrency = ConfigurationManager.AppSettings["ConvertTo"];
            var sortingField = ConfigurationManager.AppSettings["SortBy"];
            var sortDirection = ConfigurationManager.AppSettings["SortDirection"];

            var currencies = _coinMarketCapService.GetCurrencies(limit, converCurrency, sortingField, sortDirection, startPosition);

            return Mapper.Map<List<CurrencyViewModel>>(currencies);
        }
    }
}