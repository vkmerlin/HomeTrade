using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Model
{
    public class NewsCategories : IEntity
    {
        [Key]
        public int Id { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<News> News { get; set; }
    }
}
