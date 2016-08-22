using System.Collections.Generic;

namespace HRPortal.Model.ViewModels
{
    public class CreateProductViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<TradeItem> Products { get; set; }
    }
}
