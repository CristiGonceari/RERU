using CVU.ERP.Common.Data.Entities;
using RERU.Data.Entities.Enums;

namespace RERU.Data.Entities
{
    public  class SignedDocument : SoftDeleteBaseEntity
    {
        public int DocumentsForSignId { get; set; }
        public DocumentsForSign DocumentsForSign { get; set; }

        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }

        public string SignRequestId { get; set; }
        public SignRequestStatusEnum? Status { get; set; }
    }
}
