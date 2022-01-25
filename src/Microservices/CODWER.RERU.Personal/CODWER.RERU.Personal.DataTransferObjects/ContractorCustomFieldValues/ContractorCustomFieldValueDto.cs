namespace CODWER.RERU.Personal.DataTransferObjects.ContractorCustomFieldValues
{
    public class ContractorCustomFieldValueDto
    {
        public int Id { get; set; }
        public string Value { get; set; }

        public int ContractorId { get; set; }
        public string ContractorName { get; set; }

        public int ContractorTypeCustomFieldId { get; set; }
        public string ContractorTypeCustomFieldName { get; set; }
    }
}
