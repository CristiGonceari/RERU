namespace CODWER.RERU.Personal.DataTransferObjects.ContractorDriverLicenseCategories
{
    public class ContractorDriverLicenseCategoryDto
    {
        public int Id { get; set; }
        public int DriverLicenseCategoryId { get; set; }
        public string DriverLicenseCategoryName { get; set; }
        public int ContractorId { get; set; }
        public string ContractorName { get; set; }
    }
}
