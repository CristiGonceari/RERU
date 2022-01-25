namespace CODWER.RERU.Personal.DataTransferObjects.Contracts
{
    public class UpdateIndividualContractDto
    {
        public int Id { get; set; }

        public string No { get; set; }
        public int? SuperiorId { get; set; }
        public int VacationDays { get; set; }

        public int CurrencyTypeId { get; set; } // nomenclature

        public int NetSalary { get; set; }
        public int BrutSalary { get; set; }

        public int ContractorId { get; set; }
    }
}
