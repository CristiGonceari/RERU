using CVU.ERP.Common.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Entities.PersonalEntities;

namespace RERU.Data.Entities
{
    public class RegistrationFluxStep : SoftDeleteBaseEntity
    {
        public bool IsDone { get; set; }
        public bool? InProgress { get; set; }
        public RegistrationFluxStepEnum Step { get; set; }
        public int? ContractorId { get; set; }
        public Contractor Contractor { get; set; }
    }
}
