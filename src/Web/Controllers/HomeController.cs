using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using AutoMapper;
using BussinesFacade;
using BussinesFacade.Interfaces;
using BussinesFacade.Models;
using BussinesFacade.Settings;
using Web.Models;
using Web.Tools;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private ICurrencyService _coinMarketCapService;
        private IndexViewModel _indexViewModel;
        private List<CurrencyViewModel> _currencyView;
        private const string DefaultFilter = "All";

        [OutputCache(Duration = 60, Location = OutputCacheLocation.Client)]
        public ActionResult Index(string currency = "")
        {
            _coinMarketCapService = new CoinMarketCapService(new WebSettings(), new FileLogger());
            _currencyView = GetCurrencyData(_coinMarketCapService.GetCurrencies(30, "USD", 1));
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
                if (_indexViewModel.CurrencyView[0] == null)
                    _indexViewModel.CurrencyView = _currencyView;
                _indexViewModel.SelectedFilter = currency;
            }

            _indexViewModel.IsValid = _indexViewModel.CurrencyView != null;

            return View(_indexViewModel);
        }

        private List<CurrencyViewModel> GetCurrencyData(List<Data> model)
        {
            return Mapper.Map<List<CurrencyViewModel>>(model);
        }
    }
}