using CVU.ERP.Common.Data.Entities;

namespace RERU.Data.Entities
{
    public class KinshipRelationCriminalData : SoftDeleteBaseEntity
    {
        public string Text { get; set; }

        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
    }
}
