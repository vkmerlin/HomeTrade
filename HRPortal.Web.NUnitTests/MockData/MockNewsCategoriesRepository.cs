using HRPortal.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRPortal.Model;
using System.Linq.Expressions;

namespace HRPortal.Web.NUnitTests.MockData
{
    public class MockNewsCategoriesRepository : INewsCategoryRepository
    {
        List<NewsCategories> mockData;
        NewsCategories firstCat = new NewsCategories { Id = 1, CategoryName = "HR News" };
        NewsCategories secondCat = new NewsCategories { Id = 2, CategoryName = "IT News" };

        public MockNewsCategoriesRepository()
        {
            mockData = new List<NewsCategories>();
            mockData.Add(firstCat);
            mockData.Add(secondCat);
        }

        public void Delete(NewsCategories entity)
        {
            mockData.Remove(entity);
        }

        public List<NewsCategories> GetAll()
        {
            return mockData;
        }

        public List<NewsCategories> GetAll(Expression<Func<NewsCategories, bool>> where)
        {
            return mockData.AsQueryable().Where(where).ToList();
        }

        public NewsCategories GetById(int id)
        {
            return mockData.FirstOrDefault(x => x.Id == id);
        }

        public void Save(NewsCategories entity)
        {
            mockData.Add(entity);
        }

        public void Update(NewsCategories entity)
        {
            var storedData = mockData.SingleOrDefault(x => x.Id == entity.Id);
            storedData.CategoryName = entity.CategoryName;
        }
    }
}
