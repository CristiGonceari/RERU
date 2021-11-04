using CODWER.RERU.Personal.Data.Entities.NomenclatureType.NomenclatureRecords;
using CVU.ERP.Common.Data.Entities;
using System;

namespace CODWER.RERU.Personal.Data.Entities
{
    public class FamilyMember : SoftDeleteBaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }

        public int RelationId { get; set; }
        public NomenclatureRecord Relation { get; set; }

        public int ContractorId { get; set; }
        public Contractor Contractor { get; set; }
    }
}
