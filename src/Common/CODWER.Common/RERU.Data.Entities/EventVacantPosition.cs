using CVU.ERP.Common.Data.Entities;

namespace RERU.Data.Entities
{
    public class EventVacantPosition : SoftDeleteBaseEntity
    {
        public int EventId { get; set; }
        public Event Event { get; set; }

        public int CandidatePositionId { get; set; }
        public CandidatePosition CandidatePosition { get; set; }
    }
}
