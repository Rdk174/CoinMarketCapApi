using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace BussinesFacade.Models
{
    public class CurrencyInfoModel
    {
        [JsonProperty("status")]
        public Status Status { get; set; }

        [JsonProperty("data")]
        public Dictionary<string, CurrencyInfo> Data { get; set; }
    }

    public class CurrencyInfo
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
        public Platform Platform { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }
    }

    public class Platform
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("token_address")]
        public string TokenAddress { get; set; }
    }

    public class Urls
    {
        [JsonProperty("website")]
        public List<Uri> Website { get; set; }

        [JsonProperty("twitter")]
        public List<Uri> Twitter { get; set; }

        [JsonProperty("reddit")]
        public List<Uri> Reddit { get; set; }

        [JsonProperty("message_board")]
        public List<Uri> MessageBoard { get; set; }

        [JsonProperty("announcement")]
        public List<Uri> Announcement { get; set; }

        [JsonProperty("chat")]
        public List<Uri> Chat { get; set; }

        [JsonProperty("explorer")]
        public List<Uri> Explorer { get; set; }

        [JsonProperty("source_code")]
        public List<Uri> SourceCode { get; set; }
    }
}
