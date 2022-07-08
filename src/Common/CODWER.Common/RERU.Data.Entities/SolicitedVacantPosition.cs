using CVU.ERP.Common.Data.Entities;
using RERU.Data.Entities.Enums;
using System.Collections.Generic;

namespace RERU.Data.Entities
{
    public class SolicitedVacantPosition : SoftDeleteBaseEntity
    {
        public SolicitedVacantPosition()
        {
            SolicitedVacantPositionUserFiles = new HashSet<SolicitedVacantPositionUserFile>();
        }

        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }

        public int? CandidatePositionId { get; set; }
        public CandidatePosition CandidatePosition { get; set; }

        public SolicitedPositionStatusEnum SolicitedPositionStatus { get; set; }

        public virtual ICollection<SolicitedVacantPositionUserFile> SolicitedVacantPositionUserFiles { get; set; }
    }
}
