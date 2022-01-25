namespace CODWER.RERU.Personal.DataTransferObjects.ContractorCustomFieldValues
{
    public class AddEditContractorCustomFieldValueDto
    {
        public int Id { get; set; }
        public string Value { get; set; }

        public int ContractorId { get; set; }

        public int ContractorTypeCustomFieldId { get; set; }
    }
}
