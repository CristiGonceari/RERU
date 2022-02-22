using CVU.ERP.Common.Data.Entities;

namespace CODWER.RERU.Evaluation.Data.Entities
{
    public class EventLocation : SoftDeleteBaseEntity
    {
        public int EventId { get; set; }
        public Event Event { get; set; }
        
        public int LocationId { get; set; }
        public Location Location { get; set; }
    }
}
