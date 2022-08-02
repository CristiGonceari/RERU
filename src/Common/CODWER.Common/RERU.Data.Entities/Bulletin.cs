using CVU.ERP.Common.Data.Entities;
using RERU.Data.Entities.PersonalEntities;
using System;

namespace RERU.Data.Entities
{
    public class Bulletin : SoftDeleteBaseEntity
    {
        public string Series { get; set; }
        public DateTime ReleaseDay { get; set; }
        public string EmittedBy { get; set; }

        public int? ContractorId { get; set; }
        public Contractor Contractor { get; set; }

        public int? BirthPlaceId { get; set; }
        public Address BirthPlace { get; set; }

        public int? ParentsResidenceAddressId { get; set; }
        public Address ParentsResidenceAddress { get; set; }

        public int? ResidenceAddressId { get; set; }
        public Address ResidenceAddress { get; set; }
    }
}
