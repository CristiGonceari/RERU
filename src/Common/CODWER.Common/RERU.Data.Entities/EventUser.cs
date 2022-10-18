using System.Collections.Generic;
using CVU.ERP.Common.Data.Entities;

namespace RERU.Data.Entities
{
    public class EventUser : SoftDeleteBaseEntity
    {
        public EventUser()
        {
            EventUserCandidatePositions = new HashSet<EventUserCandidatePosition>();
        }

        public int EventId { get; set; }
        public Event Event { get; set; }

        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }

        public int? PositionId { get; set; }
        public CandidatePosition CandidatePosition { get; set; }

        public virtual ICollection<EventUserCandidatePosition> EventUserCandidatePositions { get; set; }
    }
}
