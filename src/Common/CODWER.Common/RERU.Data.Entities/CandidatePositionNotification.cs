using CVU.ERP.Common.Data.Entities;

namespace RERU.Data.Entities
{
    public class CandidatePositionNotification : SoftDeleteBaseEntity
    {
        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }

        public int CandidatePositionId { get; set; }
        public CandidatePosition CandidatePosition { get; set; }
    }
}
