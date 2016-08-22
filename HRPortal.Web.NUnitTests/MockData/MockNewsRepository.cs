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
    public class MockNewsRepository : INewsRepository
    {
        List<News> mockNews;
        News firstnew = new News { Id = 1, CreateDate = DateTime.Now, Title = "First piece of news", Message = "First piece of news message" };
        News secondnew = new News { Id = 2, CreateDate = DateTime.Now, Title = "Second piece of news", Message = "Second piece of news message" };
        News thirdnew = new News { Id = 3, CreateDate = DateTime.Now, Title = "Third piece of news", Message = "Third piece of news message" };

        public MockNewsRepository()
        {
            mockNews = new List<News>();
            mockNews.Add(firstnew);
            mockNews.Add(secondnew);
            mockNews.Add(thirdnew);
        }

        public void Delete(News entity)
        {
            mockNews.Remove(entity);
        }

        public List<News> GetAll()
        {
            return mockNews;
        }

        public List<News> GetAll(Expression<Func<News, bool>> where)
        {
            return mockNews.AsQueryable().Where(where).ToList();
        }

        public News GetById(int id)
        {
            return mockNews.SingleOrDefault(x => x.Id == id);
        }

        public void Save(News entity)
        {
            mockNews.Add(entity);
        }

        public void Update(News entity)
        {
            var storedData = mockNews.SingleOrDefault(x => x.Id == entity.Id);
            storedData.CreateDate = entity.CreateDate;
            storedData.Title = entity.Title;
            storedData.Message = entity.Message;
        }

        public List<News> GetNews(int skip, int take)
        {
            return mockNews.Skip(skip).Take(take).ToList();
        }
    }
}
