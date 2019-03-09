using BussinesFacade.Models;

namespace BussinesFacade.Interfaces
{
    public interface ICoinMarketCapService
    {
        CurrencyModel GetCurrencies(int limit, string convertCurrency, int startPosition);
    }
}