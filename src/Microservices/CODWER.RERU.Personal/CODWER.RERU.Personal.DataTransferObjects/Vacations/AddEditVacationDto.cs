using System;
using CODWER.RERU.Personal.Data.Entities.Enums;

namespace CODWER.RERU.Personal.DataTransferObjects.Vacations
{
    public class AddEditVacationDto
    {
        public int ContractorId { get; set; }
        public string Mentions { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public VacationType VacationType { get; set; }

        //specific fields
        public string Institution { get; set; } // for study type
        public int ChildAge { get; set; } // for child
    }
}
