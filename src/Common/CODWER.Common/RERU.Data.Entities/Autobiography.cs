using CVU.ERP.Common.Data.Entities;
using RERU.Data.Entities.PersonalEntities;

namespace RERU.Data.Entities
{
    public class Autobiography : SoftDeleteBaseEntity
    {
        public string Text { get; set; }

        public int? ContractorId { get; set; }
        public Contractor Contractor { get; set; }
    }
}
