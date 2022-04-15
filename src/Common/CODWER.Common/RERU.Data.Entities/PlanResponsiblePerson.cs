using CVU.ERP.Common.Data.Entities;

namespace RERU.Data.Entities
{
    public class PlanResponsiblePerson : SoftDeleteBaseEntity
    {
        public int PlanId { get; set; }
        public Plan Plan { get; set; }

        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
    }
}
