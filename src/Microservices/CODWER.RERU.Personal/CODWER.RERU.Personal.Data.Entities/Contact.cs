using CODWER.RERU.Personal.Data.Entities.Enums;
using CVU.ERP.Common.Data.Entities;

namespace CODWER.RERU.Personal.Data.Entities
{
    public class Contact : SoftDeleteBaseEntity
    {
        public ContactTypeEnum Type { get; set; }
        public string Value { get; set; }

        public int ContractorId { get; set; }
        public Contractor Contractor { get; set; }
    }
}
