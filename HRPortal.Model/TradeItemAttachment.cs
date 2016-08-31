using System.ComponentModel.DataAnnotations.Schema;

namespace HRPortal.Model
{
    public class TradeItemAttachment: IEntity
    {
        public int Id { get; set; }

        public int TradeItemId { get; set; }

        [NotMapped]
        public TradeItem TradeItem { get; set; }

        public string AttachementPath { get; set; }
    }
}
