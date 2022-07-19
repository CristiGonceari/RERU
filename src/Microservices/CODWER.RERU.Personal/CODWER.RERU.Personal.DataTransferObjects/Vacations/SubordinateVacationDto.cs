﻿using System;
using RERU.Data.Entities.PersonalEntities.Enums;

namespace CODWER.RERU.Personal.DataTransferObjects.Vacations
{
    public class SubordinateVacationDto
    {
        public int Id { get; set; }
        public string Mentions { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int CountDays { get; set; }
        public StageStatusEnum Status { get; set; }

        public int ContractorId { get; set; }
        public string ContractorName { get; set; }
        public string ContractorLastName { get; set; }

        public VacationType VacationType { get; set; }

        public string VacationRequestId { get; set; }
        public string VacationRequestName { get; set; }

        public string VacationOrderId { get; set; }
        public string VacationOrderName { get; set; }


        //specific fields
        public string Institution { get; set; } // for study type
        public int ChildAge { get; set; } // for child
    }
}
