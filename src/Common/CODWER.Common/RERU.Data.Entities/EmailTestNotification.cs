using CVU.ERP.Common.Data.Entities;

namespace RERU.Data.Entities
{
    public class EmailTestNotification : SoftDeleteBaseEntity
    {
        public int TestId { get; set; }
        public Test Test { get; set; }

        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
    }
}
