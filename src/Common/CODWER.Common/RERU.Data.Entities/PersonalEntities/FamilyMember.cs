using System;
using CVU.ERP.Common.Data.Entities;
using RERU.Data.Entities.PersonalEntities.NomenclatureType.NomenclatureRecords;

namespace RERU.Data.Entities.PersonalEntities
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
