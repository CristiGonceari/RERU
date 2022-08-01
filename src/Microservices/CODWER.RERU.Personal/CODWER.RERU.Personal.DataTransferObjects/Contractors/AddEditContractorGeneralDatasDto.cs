using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Personal.DataTransferObjects.Contractors
{
    public class AddEditContractorGeneralDatasDto
    {
        public int Id { get; set; }
        public int ContractorId { get; set; }
        public string WorkPhone { get; set; }
        public string HomePhone { get; set; }

        public SexTypeEnum Sex { get; set; }
        public StateLanguageLevel StateLanguageLevel { get; set; }

        public int CandidateNationalityId { get; set; }

        public int CandidateCitizenshipId { get; set; }
    }
}
