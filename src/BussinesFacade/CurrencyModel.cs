namespace BussinesFacade
{
    using System;
    using System.Collections.Generic;

    public class CurrencyModel
    {
        public Status Status { get; set; }
        public List<Data> Data { get; set; }
    }
    
    public class Status
    {
        public DateTimeOffset Timestamp { get; set; }
        public long ErrorCode { get; set; }
        public object ErrorMessage { get; set; }
        public long Elapsed { get; set; }
        public long CreditCount { get; set; }
    }

    public class Data
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Slug { get; set; }
        public long CirculatingSupply { get; set; }
        public long TotalSupply { get; set; }
        public long MaxSupply { get; set; }
        public DateTimeOffset DateAdded { get; set; }
        public long NumMarketPairs { get; set; }
        public List<string> Tags { get; set; }
        public object Platform { get; set; }
        public long CmcRank { get; set; }
        public DateTimeOffset LastUpdated { get; set; }
        public Quote Quote { get; set; }
    }

    public class Quote
    {
        public Usd Usd { get; set; }
    }

    public class Usd
    {
        public double Price { get; set; }
        public double Volume24H { get; set; }
        public double PercentChange1H { get; set; }
        public double PercentChange24H { get; set; }
        public double PercentChange7D { get; set; }
        public double MarketCap { get; set; }
        public DateTimeOffset LastUpdated { get; set; }
    }
}