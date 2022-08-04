using CVU.ERP.Common.Data.Entities;

namespace RERU.Data.Entities
{
    public class EventUser : SoftDeleteBaseEntity
    {
        public int EventId { get; set; }
        public Event Event { get; set; }

        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }

        public int? PositionId { get; set; }
        public CandidatePosition CandidatePosition { get; set; }
    }
}
