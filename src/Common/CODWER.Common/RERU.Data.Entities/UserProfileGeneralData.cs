using CVU.ERP.Common.Data.Entities;
using RERU.Data.Entities.Enums;

namespace RERU.Data.Entities
{
    public class UserProfileGeneralData : SoftDeleteBaseEntity
    {
        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }

        public string WorkPhone { get; set; }
        public string HomePhone { get; set; }

        public SexTypeEnum Sex { get; set; }
        public StateLanguageLevel StateLanguageLevel { get; set; }

        public int CandidateNationalityId { get; set; }
        public CandidateNationality CandidateNationality { get; set; }

        public int CandidateCitizenshipId { get; set; }
        public CandidateCitizenship CandidateCitizenship { get; set; }
    }
}
