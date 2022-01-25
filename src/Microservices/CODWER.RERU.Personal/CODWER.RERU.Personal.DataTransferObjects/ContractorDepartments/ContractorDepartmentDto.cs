using System;

namespace CODWER.RERU.Personal.DataTransferObjects.ContractorDepartments
{
    public class ContractorDepartmentDto
    {
        public int Id { get; set; }
        public int ContractorId { get; set; }
        public string ContractorName { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public DateTime FromDate { get; set; }
    }
}
