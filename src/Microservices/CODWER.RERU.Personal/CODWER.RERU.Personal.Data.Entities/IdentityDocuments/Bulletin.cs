using CVU.ERP.Common.Data.Entities;
using System;

namespace CODWER.RERU.Personal.Data.Entities.IdentityDocuments
{
    public class Bulletin : SoftDeleteBaseEntity
    {
        public string Series { get; set; }
        public DateTime ReleaseDay { get; set; }
        public string EmittedBy { get; set; }
        public string Idnp { get; set; }

        public int ContractorId { get; set; }
        public Contractor Contractor { get; set; }

        public int? BirthPlaceId { get; set; }
        public Address BirthPlace { get; set; }

        public int? LivingAddressId { get; set; }
        public Address LivingAddress { get; set; }

        public int? ResidenceAddressId { get; set; }
        public Address ResidenceAddress { get; set; }
    }

}
