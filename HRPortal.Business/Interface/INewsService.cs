using HRPortal.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HRPortal.Business
{
    public interface INewsService
    {
        Task CreateNews(News news);
        IEnumerable<News> GetNews(int pageNum = 0, bool position = false);
        IEnumerable<News> GetAll(Expression<Func<News, bool>> where);
        News GetById(int id);
        Task EditNews(News newNews);
        Task RemoveNews(News pieceOfNews);

        IEnumerable<NewsCategories> GetNewsCategories();

        Task RemoveAttachment(int id);

        Task RemoveComment(int id);

        Task RemoveReply(int id);

        Task CreateNewsComment(NewsComments newComment);
        Task CreateNewsReply(NewsReply newReply);
    }
}
