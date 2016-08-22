using HRPortal.Data;
using HRPortal.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace HRPortal.Business
{
    public class TradeService : ITradeService
    {
        ICategoryRepository categoryDao;
        ITradeItemRepository itemDao;

        public TradeService(ICategoryRepository categoryDao, ITradeItemRepository itemDao)
        {
            this.categoryDao = categoryDao;
            this.itemDao = itemDao;
        }

        public async Task CreateCategoryAsync(Category newCat)
        {
            await Task.Run(() => categoryDao.Save(newCat));
        }

        public void UpdateCategory(Category cat)
        {
            categoryDao.Update(cat);
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return categoryDao.GetAll();
        }

        public void RemoveCategory(Category cat)
        {
            categoryDao.Delete(cat);
        }

        public IEnumerable<TradeItem> GetAllProducts()
        {
            return itemDao.GetAll();
        }

        public async Task CreateProductAsync(TradeItem newItem)
        {
            await Task.Run(() => itemDao.Save(newItem));
        }

        public void RemoveProduct(TradeItem item)
        {
            itemDao.Delete(itemDao.GetById(item.Id));
        }

        public void UpdateProduct(TradeItem item)
        {
            var prod = itemDao.GetById(item.Id);
            if (prod != null)
            {
                prod.Name = item.Name;
                prod.CategoryId = item.CategoryId;
                prod.Description = item.Description;
                prod.Price = item.Price;
                itemDao.Update(prod);
            }
        }

        public TradeItem GetProductById(int id)
        {
            return itemDao.GetById(id);
        }
    }
}
