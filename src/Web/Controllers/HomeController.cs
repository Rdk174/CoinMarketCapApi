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
        private readonly ICurrencyService _coinMarketCapService;
        private IndexViewModel _indexViewModel;
        private List<CurrencyViewModel> _currencyView;
        private readonly ISettings _webSettings;
        private readonly ILogger _fileLogger;

        private const string DefaultFilter = "All";
        private const string DefaultConvertTo = "USD";
        private const int DefaultLimit = 10;
        private const int DefaultStartPosition = 1;


        public HomeController()
        {
            _webSettings = new WebSettings();
            _fileLogger=new FileLogger();

            _coinMarketCapService = new CoinMarketCapService(_webSettings, _fileLogger);
        }

        [OutputCache(Duration = 60, Location = OutputCacheLocation.Client)]
        public ActionResult Index(string currency = "")
        {
            int.TryParse(_webSettings.GetSettings("Limit", DefaultLimit.ToString()), out var limit);
            int.TryParse(_webSettings.GetSettings("StartPosition", DefaultStartPosition.ToString()), out var startPosition);

            _currencyView = Mapper.Map<List<CurrencyViewModel>>(_coinMarketCapService.GetCurrencies(limit, _webSettings.GetSettings("ConvertTo", DefaultConvertTo), startPosition));

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
                {
                    _indexViewModel.CurrencyView = _currencyView;
                }

                _indexViewModel.SelectedFilter = currency;
            }

            _indexViewModel.IsValid = _indexViewModel.CurrencyView != null;

            return View(_indexViewModel);
        }
    }
}