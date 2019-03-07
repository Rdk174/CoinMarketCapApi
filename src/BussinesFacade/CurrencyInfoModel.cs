using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace BussinesFacade
{
    public class CurrencyInfoModel
    {
        [JsonProperty("status")]
        public Status Status { get; set; }
        [JsonProperty("data")]
        public List<CurrencyInfo> Data { get; set; }
    }

    public class CurrencyInfo
    {
        public ItemInfo Item { get; set; }
    }

    public class ItemInfo
    {
        [JsonProperty("urls")]
        public Urls Urls { get; set; }
        [JsonProperty("logo")]
        public Uri Logo { get; set; }
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("symbol")]
        public string Symbol { get; set; }
        [JsonProperty("slug")]
        public string Slug { get; set; }
        [JsonProperty("date_added")]
        public DateTimeOffset DateAdded { get; set; }
        [JsonProperty("tags")]
        public List<string> Tags { get; set; }
        [JsonProperty("platform")]
        public object Platform { get; set; }
        [JsonProperty("category")]
        public string Category { get; set; }
    }

    public class Urls
    {
        [JsonProperty("website")]
        public List<Uri> Website { get; set; }
        [JsonProperty("twitter")]
        public List<object> Twitter { get; set; }
        [JsonProperty("reddit")]
        public List<Uri> Reddit { get; set; }
        [JsonProperty("message_board")]
        public List<Uri> MessageBoard { get; set; }
        [JsonProperty("announcement")]
        public List<object> Announcement { get; set; }
        [JsonProperty("chat")]
        public List<object> Chat { get; set; }
        [JsonProperty("explorer")]
        public List<Uri> Explorer { get; set; }
        [JsonProperty("source_code")]
        public List<Uri> SourceCode { get; set; }
    }

}