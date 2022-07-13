using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Core.DataTransferObjects.UserProfileGeneralDatas
{
    public class UserProfileGeneralDataDto
    {
        public int Id { get; set; }
        public int UserProfileId { get; set; }
        public string WorkPhone { get; set; }
        public string HomePhone { get; set; }

        public SexTypeEnum Sex { get; set; }
        public StateLanguageLevel StateLanguageLevel { get; set; }

        public int CandidateNationalityId { get; set; }

        public int CandidateCitizenshipId { get; set; }
    }
}
