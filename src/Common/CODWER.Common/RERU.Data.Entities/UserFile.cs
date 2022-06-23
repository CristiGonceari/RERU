using CVU.ERP.Common.Data.Entities;

namespace RERU.Data.Entities
{
    public class UserFile : SoftDeleteBaseEntity
    {
        public UserFile(int userProfileId, string fileId)
        {
            UserProfileId = userProfileId;
            FileId = fileId;
        }

        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }

        public string FileId { get; set; }

        public int? RequiredDocumentId { get; set; }
        public RequiredDocument RequiredDocument { get; set; }
    }
}
