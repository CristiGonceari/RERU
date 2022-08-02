using CVU.ERP.Common.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Entities.PersonalEntities;
using System;

namespace RERU.Data.Entities
{
    public class MilitaryObligation : SoftDeleteBaseEntity
    {
        public MilitaryObligationTypeEnum MilitaryObligationType { get; set; }
        public DateTime MobilizationYear { get; set; }
        public DateTime WithdrawalYear { get; set; }
        public string Efectiv { get; set; }
        public string MilitarySpecialty { get; set; }
        public string Degree { get; set; }

        public string MilitaryBookletSeries { get; set; }
        public int MilitaryBookletNumber { get; set; }
        public DateTime MilitaryBookletReleaseDay { get; set; }
        public string MilitaryBookletEminentAuthority { get; set; }

        public int? ContractorId { get; set; }
        public Contractor Contractor { get; set; }
    }
}
