using System;
using System.Collections.Generic;

namespace BussinesFacade
{
    public partial class CurrencyInfoModel
    {
        public Status Status { get; set; }
        public CurrencyInfo Data { get; set; }
    }

    public partial class CurrencyInfo
    {
        public ItemInfo Item { get; set; }
    }

    public partial class ItemInfo
    {
        public Urls Urls { get; set; }
        public Uri Logo { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Slug { get; set; }
        public DateTimeOffset DateAdded { get; set; }
        public List<string> Tags { get; set; }
        public object Platform { get; set; }
        public string Category { get; set; }
    }

    public partial class Urls
    {
        public List<Uri> Website { get; set; }
        public List<object> Twitter { get; set; }
        public List<Uri> Reddit { get; set; }
        public List<Uri> MessageBoard { get; set; }
        public List<object> Announcement { get; set; }
        public List<object> Chat { get; set; }
        public List<Uri> Explorer { get; set; }
        public List<Uri> SourceCode { get; set; }
    }

}