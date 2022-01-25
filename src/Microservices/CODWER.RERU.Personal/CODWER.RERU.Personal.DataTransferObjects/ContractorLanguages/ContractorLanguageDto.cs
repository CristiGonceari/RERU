namespace CODWER.RERU.Personal.DataTransferObjects.ContractorLanguages
{
    public class ContractorLanguageDto
    {
        public int Id { get; set; }
        public int ContractorId { get; set; }
        public string ContractorName { get; set; }
        public int LanguageId { get; set; }
        public string LanguageName { get; set; }
        public int LanguageLevelId { get; set; }
        public string LanguageLevelName { get; set; }
    }
}
