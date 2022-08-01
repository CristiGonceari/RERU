using CVU.ERP.Common.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Entities.PersonalEntities;

namespace RERU.Data.Entities
{
    public class KinshipRelationWithUserProfile : SoftDeleteBaseEntity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Function { get; set; }
        public KinshipDegreeEnum KinshipDegree { get; set; }
        public string Subdivision { get; set; }

        public int? ContractorId { get; set; }
        public Contractor Contractor { get; set; }
    }
}
