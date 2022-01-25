namespace CODWER.RERU.Personal.DataTransferObjects.ContractorLanguages
{
    public class AddEditContractorLanguageDto
    {
        public int Id { get; set; }
        public int ContractorId { get; set; }
        public int LanguageId { get; set; }
        public int LanguageLevelId { get; set; }
    }
}
