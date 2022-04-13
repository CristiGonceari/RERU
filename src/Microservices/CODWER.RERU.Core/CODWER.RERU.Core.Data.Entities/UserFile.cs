using CVU.ERP.Common.Data.Entities;

namespace CODWER.RERU.Core.Data.Entities
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
    }
}
