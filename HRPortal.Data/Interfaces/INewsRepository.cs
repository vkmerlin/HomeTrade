using HRPortal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Data
{
    public interface INewsRepository : IGenericRepository<News>
    {
        List<News> GetNews(int skip, int take);
    }
}
