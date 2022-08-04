using CVU.ERP.Common.Data.Entities;
using RERU.Data.Entities.PersonalEntities;

namespace RERU.Data.Entities
{
    public class MaterialStatus : SoftDeleteBaseEntity
    {

        public int MaterialStatusTypeId { get; set; }
        public MaterialStatusType MaterialStatusType { get; set; }

        public int? ContractorId { get; set; }
        public Contractor Contractor { get; set; }
    }
}
