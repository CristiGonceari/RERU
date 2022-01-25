using System;
using CODWER.RERU.Personal.Data.Entities.Enums;

namespace CODWER.RERU.Personal.DataTransferObjects.Positions
{
    public class AddEditPositionDto
    {
        public int Id { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime GeneratedDate { get; set; }
        public string WorkPlace { get; set; }


        public int ProbationDayPeriod { get; set; }
        public WorkHoursEnum WorkHours { get; set; }

        public int OrganizationRoleId { get; set; }
        public int DepartmentId { get; set; }
        public int ContractorId { get; set; }
    }
}
