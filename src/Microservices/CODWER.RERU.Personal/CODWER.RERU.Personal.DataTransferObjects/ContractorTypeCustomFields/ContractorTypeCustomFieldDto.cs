namespace CODWER.RERU.Personal.DataTransferObjects.ContractorTypeCustomFields
{
    public class ContractorTypeCustomFieldDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int ContractorTypeId { get; set; }
        public string ContractorTypeName { get; set; }
    }
}
