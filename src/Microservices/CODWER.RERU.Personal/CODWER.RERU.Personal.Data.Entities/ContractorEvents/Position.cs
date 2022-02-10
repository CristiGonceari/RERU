using CODWER.RERU.Personal.Data.Entities.Enums;
using CVU.ERP.Common.Data.Entities;
using System;
using System.Collections.Generic;

namespace CODWER.RERU.Personal.Data.Entities.ContractorEvents
{
    public class Position : SoftDeleteBaseEntity
    {
        public Position()
        {
            DismissalRequests = new HashSet<DismissalRequest>();
        }
        public DateTime? FromDate { get; set; }
        public DateTime? GeneratedDate { get; set; }
        public DateTime? ToDate { get; set; }

        public string WorkPlace { get; set; }

        public int ProbationDayPeriod { get; set; }
        public WorkHoursEnum WorkHours { get; set; }

        public string No { get; set; }

        public int? OrganizationRoleId { get; set; }
        public OrganizationRole OrganizationRole { get; set; }

        public int? DepartmentId { get; set; }
        public Department Department { get; set; }

        public int ContractorId { get; set; }
        public Contractor Contractor { get; set; }

        public string OrderId { get; set; }

        public ICollection<DismissalRequest> DismissalRequests { get; set; }
    }
}
