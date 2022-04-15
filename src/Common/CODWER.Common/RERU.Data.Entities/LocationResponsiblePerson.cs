using CVU.ERP.Common.Data.Entities;

namespace RERU.Data.Entities
{
    public class LocationResponsiblePerson : SoftDeleteBaseEntity
    {
        public int LocationId { get; set; }
        public Location Location { get; set; }
        
        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
    }
}
