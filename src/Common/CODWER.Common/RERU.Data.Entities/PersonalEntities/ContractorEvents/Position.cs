using System;
using System.Collections.Generic;
using CVU.ERP.Common.Data.Entities;
using RERU.Data.Entities.PersonalEntities.Enums;

namespace RERU.Data.Entities.PersonalEntities.ContractorEvents
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

        public int? RoleId { get; set; }
        public Role Role { get; set; }

        public int? DepartmentId { get; set; }
        public Department Department { get; set; }

        public int ContractorId { get; set; }
        public Contractor Contractor { get; set; }

        public string OrderId { get; set; }

        public ICollection<DismissalRequest> DismissalRequests { get; set; }
    }
}
