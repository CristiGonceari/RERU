using CVU.ERP.Common.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Entities.PersonalEntities;
using System;

namespace RERU.Data.Entities
{
    public class KinshipRelation : SoftDeleteBaseEntity
    {
        public KinshipDegreeEnum KinshipDegree { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthLocation { get; set; }
        public string Function { get; set; }
        public string WorkLocation { get; set; }
        public string ResidenceAddress { get; set; }

        public int? ContractorId { get; set; }
        public Contractor Contractor { get; set; }
    }
}
