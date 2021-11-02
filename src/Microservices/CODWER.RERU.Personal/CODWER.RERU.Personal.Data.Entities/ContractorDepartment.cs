using CVU.ERP.Common.Data.Entities;
using System;

namespace CODWER.RERU.Personal.Data.Entities
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
