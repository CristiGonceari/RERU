using System;

namespace CODWER.RERU.Personal.DataTransferObjects.ContractorDepartments
{
    public class AddEditContractorDepartmentDto
    {
        public int Id { get; set; }
        public int ContractorId { get; set; }
        public int DepartmentId { get; set; }
        public DateTime FromDate { get; set; }
    }
}
