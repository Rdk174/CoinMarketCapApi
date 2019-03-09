using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using AutoMapper;
using BussinesFacade;
using BussinesFacade.Defenitions;
using BussinesFacade.Extensions;
using BussinesFacade.Models;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private CoinMarketCapService _coinMarketCapService;
        private IndexViewModel _indexViewModel;
        private List<CurrencyViewModel> _currencyView;
        private CurrencyModel _currencyModel;
        private const int Limit = 30;
        private const int StartPosition = 1;
        private const string DefaultFilter = "All";

        [OutputCache(Duration = 60, Location = OutputCacheLocation.Client)]
        public ActionResult Index(string currency="")
        {
            _coinMarketCapService = new CoinMarketCapService();
            _currencyModel = GetCurrencyModel();
            _currencyView = GetCurrencyData();
            _indexViewModel = new IndexViewModel
            {
                CurrencyView = _currencyView,
                SelectedFilter = DefaultFilter
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

        private CurrencyModel GetCurrencyModel()
        {
            var limit = Limit;
            var startPosition = StartPosition;
            var converCurrency = ConvertCurrency.USD.GetStringValue();
            var sortingField = SortingFields.MarketCap.GetStringValue();
            var sortDirection = SortDirrections.Desc.GetStringValue();

            return _coinMarketCapService.GetCurrencies(limit, converCurrency, sortingField, sortDirection, startPosition);
        }
        private List<CurrencyViewModel> GetCurrencyData()
        {
            return Mapper.Map<List<CurrencyViewModel>>(_currencyModel.Data);
        }

        private StatusViewModel GetStatus()
        {
            return Mapper.Map<StatusViewModel>(_currencyModel.Status);
        }
    }
}