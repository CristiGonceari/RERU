using CVU.ERP.Common.Data.Entities;
using RERU.Data.Entities.Enums;

namespace RERU.Data.Entities
{
    public class RegistrationFluxStep : SoftDeleteBaseEntity
    {
        public bool IsDone { get; set; }
        public RegistrationFluxStepEnum Step { get; set; }
        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
    }
}
