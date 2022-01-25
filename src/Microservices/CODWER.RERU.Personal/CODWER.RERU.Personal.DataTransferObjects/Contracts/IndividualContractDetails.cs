using System.Collections.Generic;
using CODWER.RERU.Personal.DataTransferObjects.Instructions;

namespace CODWER.RERU.Personal.DataTransferObjects.Contracts
{
    public class IndividualContractDetails
    {
        public int Id { get; set; }

        public string No { get; set; }
        public int? SuperiorId { get; set; }
        public int VacationDays { get; set; }

        public int CurrencyTypeId { get; set; } // nomenclature

        public int NetSalary { get; set; }
        public int BrutSalary { get; set; }

        public int ContractorId { get; set; }

        public List<AddEditInstructionDto> Instructions { get; set; }
    }
}