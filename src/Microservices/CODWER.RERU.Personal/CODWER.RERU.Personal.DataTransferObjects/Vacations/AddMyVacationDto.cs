using System;
using RERU.Data.Entities.PersonalEntities.Enums;

namespace CODWER.RERU.Personal.DataTransferObjects.Vacations
{
    public class AddMyVacationDto
    {
        public string Mentions { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public VacationType VacationType { get; set; }

        //specific fields
        public string Institution { get; set; } // for study type
        public int ChildAge { get; set; } // for child
    }
}
