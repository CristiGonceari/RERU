using RERU.Data.Entities.Enums;
using System;

namespace CODWER.RERU.Core.DataTransferObjects.Users
{
    public class EditCandidateDto
    {
        public int Id { get; set; }
        public int WorkPhone { get; set; }
        public int HomePhone { get; set; }
        public int MobilePhone { get; set; }
        public DateTime BirthDate { get; set; }

        public SexTypeEnum Sex { get; set; }
        public StateLanguageLevel StateLanguageLevel { get; set; }

        public int CandidateNationalityId { get; set; }
        public int CandidateCitizenshipId { get; set; }
    }
}
