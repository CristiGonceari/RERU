namespace CODWER.RERU.Personal.DataTransferObjects.ContractorTypeCustomFields
{
    public class AddEditContractorTypeCustomFieldDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int ContractorTypeId { get; set; }
    }
}