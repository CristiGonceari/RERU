using CVU.ERP.Common.Data.Entities;

namespace CODWER.RERU.Evaluation.Data.Entities
{
    public class LocationResponsiblePerson : SoftDeleteBaseEntity
    {
        public int LocationId { get; set; }
        public int UserProfileId { get; set; }
        public Location Location { get; set; }
        public UserProfile UserProfile { get; set; }
    }
}
