using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace HRPortal.Model
{
    public class News : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int NewsCategoryId { get; set; }

        [Required]
        public string Title { get; set; }
        public DateTime CreateDate { get; set; }

        [Required]
        public string Message { get; set; }
        public bool IsPin { get; set; }

        public NewsCategories NewsCategory { get; set; }

        public virtual ICollection<NewsAttachedFiles> AttachedFiles { get; set; }

        public virtual ICollection<NewsComments> Comments { get; set; }
    }
}
