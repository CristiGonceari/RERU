using CVU.ERP.Common.Data.Entities;

namespace RERU.Data.Entities
{
    public class RequiredDocumentPosition : SoftDeleteBaseEntity
    {
        public int CandidatePositionId { get; set; }
        public CandidatePosition CandidatePosition { get; set; }

        public int RequiredDocumentId { get; set; }
        public RequiredDocument RequiredDocument { get; set; }
    }
}
