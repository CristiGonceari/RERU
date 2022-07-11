using CVU.ERP.Common.Data.Entities;

namespace RERU.Data.Entities
{
    public class SolicitedVacantPositionUserFile : SoftDeleteBaseEntity
    {
        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }

        public string FileId { get; set; }

        public int SolicitedVacantPositionId { get; set; }
        public SolicitedVacantPosition SolicitedVacantPosition { get; set; }

        public int RequiredDocumentId { get; set; }
        public RequiredDocument RequiredDocument { get; set; }
    }
}
