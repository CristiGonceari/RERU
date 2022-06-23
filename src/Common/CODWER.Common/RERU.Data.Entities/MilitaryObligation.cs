using CVU.ERP.Common.Data.Entities;
using RERU.Data.Entities.Enums;
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

        public MilitaryBooklet MilitaryBooklet { get; set; }

        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
    }
}
