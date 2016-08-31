using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Model
{
    public class TradeItem: IEntity
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public DateTime CreateDate { get; set; }

        public virtual ICollection<TradeItemAttachment> Attachments { get; set; }
    }
}
