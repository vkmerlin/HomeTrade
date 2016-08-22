using HRPortal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Data
{
    public class NewsRepository : GenericRepository<News>, INewsRepository
    {
        HRPortalContext db;
        public NewsRepository()
        {
            db = new HRPortalContext();
        }

        public List<News> GetNews(int skip, int take)
        {
            return db.Set<News>().OrderByDescending(x => x.CreateDate).Where(x => !x.IsPin).Skip(skip).Take(take).ToList();
        }
    }
}
