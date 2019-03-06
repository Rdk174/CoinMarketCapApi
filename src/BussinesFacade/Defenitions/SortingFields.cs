using BussinesFacade.Attributes;

namespace BussinesFacade.Defenitions
{
    public enum SortingFields
    {
        [StringValue("name")]
        Name,
        [StringValue("symbol")]
        Symbol,
        [StringValue("date_added")]
        DateAdded,
        [StringValue("market_cap")]
        MarketCap,
        [StringValue("price")]
        Price

    }
}