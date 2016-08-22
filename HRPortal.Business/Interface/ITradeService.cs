using HRPortal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Business
{
    public interface ITradeService
    {
        IEnumerable<Category> GetAllCategories();
        Task CreateCategoryAsync(Category newCat);
        void RemoveCategory(Category cat);

        void UpdateCategory(Category cat);

        IEnumerable<TradeItem> GetAllProducts();
        Task CreateProductAsync(TradeItem newItem);
        void RemoveProduct(TradeItem item);

        void UpdateProduct(TradeItem item);

        TradeItem GetProductById(int id);
    }
}
