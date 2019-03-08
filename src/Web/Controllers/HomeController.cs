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
        private List<CurrencyViewModel> _currencyView;
        private const string _defaultFilter = "All";

        [OutputCache(Duration = 60, Location = OutputCacheLocation.Client)]
        public ActionResult Index(string currency="")
        {
            _coinMarketCapService = new CoinMarketCapService();
            _currencyView = GetCurrencyData();
            _indexViewModel = new IndexViewModel
            {
                CurrencyView = _currencyView,
                SelectedFilter = _defaultFilter
            };
            if (!string.IsNullOrEmpty(currency))
            {
                _indexViewModel.CurrencyView = new List<CurrencyViewModel>()
                {
                    (_indexViewModel.CurrencyView.FirstOrDefault(x =>
                        string.Equals(x.Name, currency, StringComparison.OrdinalIgnoreCase)))
                };
                if (_indexViewModel.CurrencyView[0] == null) _indexViewModel.CurrencyView = _currencyView;
                _indexViewModel.SelectedFilter = currency;
            }
            
            if (_indexViewModel.CurrencyView==null)
            {
               return RedirectToAction("Error", "Error", GetStatus() );
            }

            return View(_indexViewModel);
        }

        private CurrencyModel GetCurrenciesModel()
        {
            int.TryParse(ConfigurationManager.AppSettings["CryptoCurrencyListLimit"], out var limit);
            int.TryParse(ConfigurationManager.AppSettings["StartPosition"], out var startPosition);
            var converCurrency = ConfigurationManager.AppSettings["ConvertTo"];
            var sortingField = ConfigurationManager.AppSettings["SortBy"];
            var sortDirection = ConfigurationManager.AppSettings["SortDirection"];

            return _coinMarketCapService.GetCurrencies(limit, converCurrency, sortingField, sortDirection, startPosition);
        }
        private List<CurrencyViewModel> GetCurrencyData()
        {
            var currencies = GetCurrenciesModel();
            return Mapper.Map<List<CurrencyViewModel>>(currencies.Data);
        }

        private StatusViewModel GetStatus()
        {
            var status = GetCurrenciesModel();
            return Mapper.Map<StatusViewModel>(status.Status);
        }
    }
}