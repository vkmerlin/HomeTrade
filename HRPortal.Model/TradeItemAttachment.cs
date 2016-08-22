using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Model
{
    public class TradeItemAttachment: IEntity
    {
        public int Id { get; set; }

        public int TradeItemId { get; set; }

        public TradeItem TradeItem { get; set; }

        public string AttachementPath { get; set; }
    }
}
