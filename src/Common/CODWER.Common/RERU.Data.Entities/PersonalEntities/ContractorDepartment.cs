using System;
using CVU.ERP.Common.Data.Entities;

namespace RERU.Data.Entities.PersonalEntities
{
    public class ContractorDepartment : SoftDeleteBaseEntity
    {
        public DateTime FromDate { get; set; }

        public int ContractorId { get; set; }
        public Contractor Contractor { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
