using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Model
{
    public class NewsReply:IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int NewsCommentsId { get; set; }

        public DateTime CreateDate { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
