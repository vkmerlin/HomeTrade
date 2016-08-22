using HRPortal.Business.Enums;
using HRPortal.Data;
using HRPortal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HRPortal.Business
{
    public class NewsService : INewsService
    {
        INewsRepository newsDao;
        INewsCategoryRepository newsCatsDao;
        INewsAttachmentsRepository newsAttachmentsDao;
        INewsCommentsRepository newsCommentsDao;
        INewsReplyRepository newsReplyDao;
        public NewsService(INewsRepository newsDao, INewsCategoryRepository newsCatsDao, INewsAttachmentsRepository newsAttachmentsDao, 
            INewsCommentsRepository newsCommentsDao, INewsReplyRepository newsReplyDao)
        {
            this.newsDao = newsDao;
            this.newsCatsDao = newsCatsDao;
            this.newsAttachmentsDao = newsAttachmentsDao;
            this.newsCommentsDao = newsCommentsDao;
            this.newsReplyDao = newsReplyDao;
        }

        public async Task CreateNews(News news)
        {
            await Task.Run(() => newsDao.Save(news));
        }

        public async Task EditNews(News newNews)
        {
            var oldNews = newsDao.GetById(newNews.Id);
            if (oldNews != null)
            {
                oldNews.Title = newNews.Title;
                oldNews.Message = newNews.Message;
                oldNews.IsPin = newNews.IsPin;
                oldNews.NewsCategoryId = newNews.NewsCategoryId;
                if (newNews.AttachedFiles.Any())
                {
                    foreach (var attachment in newNews.AttachedFiles)
                    {
                        if (oldNews.AttachedFiles.Count < 5)
                            oldNews.AttachedFiles.Add(attachment);
                    }
                }
                await Task.Run(() => newsDao.Update(oldNews));
            }
        }

        public List<News> GetAll()
        {
            return newsDao.GetAll();
        }

        public List<News> GetAll(Expression<Func<News, bool>> where)
        {
            return newsDao.GetAll(where);
        }

        public News GetById(int id)
        {
            return newsDao.GetById(id);
        }

        public List<NewsCategories> GetNewsCategories()
        {
            return newsCatsDao.GetAll();
        }

        public async Task RemoveNews(News pieceOfNews)
        {
            await Task.Run(() => newsDao.Delete(pieceOfNews));
        }

        public List<News> GetNews(int pageNum = 0, bool position = false)
        {
            if (position) {
                List<News> news = newsDao.GetAll(x => x.IsPin);
                news.AddRange(newsDao.GetNews(0, pageNum));
                return news;
            }

            var newsNumber = (int)NewsPerPage.Small;
            if (pageNum == 0)
            {
                List<News> news = newsDao.GetAll(x => x.IsPin);
                if (news.Count >= newsNumber)
                    return news;
                else
                    news.AddRange(newsDao.GetNews(0, newsNumber - news.Count));
                return news;
            }
            else {
                return newsDao.GetNews(pageNum, newsNumber);
            }
        }

        public async Task RemoveAttachment(int id)
        {
            var attachement = newsAttachmentsDao.GetById(id);
            if (attachement != null)
                await Task.Run(() => newsAttachmentsDao.Delete(attachement));
        }

        public async Task CreateNewsComment(NewsComments newComment)
        {
            await Task.Run(() => newsCommentsDao.Save(newComment));
        }

        public async Task CreateNewsReply(NewsReply newReply)
        {
            await Task.Run(() => newsReplyDao.Save(newReply));
        }
    }
}
