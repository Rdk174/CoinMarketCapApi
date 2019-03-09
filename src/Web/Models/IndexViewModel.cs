using System.Collections.Generic;

namespace Web.Models
{
    public class IndexViewModel
    {
        public string SelectedFilter { get; set; }
        public List<CurrencyViewModel> CurrencyView { get; set; }
        public bool IsValid { get; set; } = true;
    }
}