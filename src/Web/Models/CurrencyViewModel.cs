using System;

namespace Web.Models
{
    public class CurrencyViewModel
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Symbol { get; set; }
        public string Price { get; set; }
        public double PercentChange1H { get; set; }
        public double PercentChange24H { get; set; }
        public double MarketCap { get; set; }
        public DateTimeOffset LastUpdated { get; set; }
    }
}