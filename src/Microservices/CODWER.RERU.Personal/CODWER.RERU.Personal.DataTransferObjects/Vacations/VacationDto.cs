using System;
using CODWER.RERU.Personal.Data.Entities.Enums;

namespace CODWER.RERU.Personal.DataTransferObjects.Vacations
{
    public class VacationDto
    {
        public int Id { get; set; }
        public string Reason { get; set; }
        public string Mentions { get; set; }
        public StageStatusEnum Status { get; set; }
        public int CountDays { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public int ContractorId { get; set; }
        public string ContractorName { get; set; }

        public int VacationTypeId { get; set; }
        public string VacationTypeName { get; set; }

        public int VacationRequestId { get; set; }
        public string VacationRequestName { get; set; }

        public int VacationOrderId { get; set; }
        public string VacationOrderName { get; set; }

        //specific fields
        public string Institution { get; set; } // for study type
        public int ChildAge { get; set; } // for child

    }
}
