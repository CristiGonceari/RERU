using RERU.Data.Entities.Enums;
using System;

namespace CODWER.RERU.Core.DataTransferObjects.MilitaryObligation
{
    public class MilitaryObligationDto
    {
        public int Id { get; set; }
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

        public int UserProfileId { get; set; }
    }
}
