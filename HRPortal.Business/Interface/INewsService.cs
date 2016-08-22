using HRPortal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Business
{
    public interface INewsService
    {
        Task CreateNews(News news);
        List<News> GetNews(int pageNum = 0, bool position = false);
        List<News> GetAll(Expression<Func<News, bool>> where);
        News GetById(int id);
        Task EditNews(News newNews);
        Task RemoveNews(News pieceOfNews);

        List<NewsCategories> GetNewsCategories();

        Task RemoveAttachment(int id);

        Task CreateNewsComment(NewsComments newComment);
        Task CreateNewsReply(NewsReply newReply);
    }
}
