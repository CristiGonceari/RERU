using CVU.ERP.Common.Data.Entities;

namespace RERU.Data.Entities
{
    public class EventUserCandidatePosition : SoftDeleteBaseEntity
    {
        public int EventUserId { get; set; }
        public EventUser EventUser { get; set; }

        public int? CandidatePositionId { get; set; }
        public CandidatePosition CandidatePosition { get; set; }
    }
}
