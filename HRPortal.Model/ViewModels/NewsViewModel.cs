using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Model.ViewModels
{
    public class NewsViewModel
    {
        public int NewsCategoryId { get; set; }
        public IEnumerable<NewsCategories> NewsCategories { get; set; }
    }
}
