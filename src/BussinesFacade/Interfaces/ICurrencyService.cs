using System.Collections.Generic;
using BussinesFacade.Models;

namespace BussinesFacade.Interfaces
{
    public interface ICurrencyService
    {
        List<Data> GetCurrencies(int limit, string convertCurrency, int startPosition);
    }
}