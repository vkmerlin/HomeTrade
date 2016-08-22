using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Model
{
    public class NewsAttachedFiles : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int NewsId { get; set; }

        public string FilePath { get; set; }

        [NotMapped]
        public News News { get; set; }
    }
}
