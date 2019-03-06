using Newtonsoft.Json;

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

        [JsonIgnore]
        public string Logo { get; set; }
    }

    public class Quote
    {
        public Usd Usd { get; set; }
    }

    public class Usd
    {
        [JsonProperty("price")]
        public double Price { get; set; }
        [JsonProperty("volume_24h")]
        public double Volume24H { get; set; }
        [JsonProperty("percent_change_1h")]
        public double PercentChange1H { get; set; }
        [JsonProperty("percent_change_24h")]
        public double PercentChange24H { get; set; }
        public double PercentChange7D { get; set; }
        [JsonProperty("market_cap")]
        public double MarketCap { get; set; }

        [JsonProperty("last_updated")]
        public DateTimeOffset LastUpdated { get; set; }
    }
}