using CVU.ERP.Common.Data.Entities;
using RERU.Data.Entities.PersonalEntities.Enums;

namespace RERU.Data.Entities.PersonalEntities
{
    public class Contact : SoftDeleteBaseEntity
    {
        public ContactTypeEnum Type { get; set; }
        public string Value { get; set; }

        public int ContractorId { get; set; }
        public Contractor Contractor { get; set; }
    }
}
