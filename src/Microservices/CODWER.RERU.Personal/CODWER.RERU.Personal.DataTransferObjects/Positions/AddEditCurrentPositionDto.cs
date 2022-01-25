using System;

namespace CODWER.RERU.Personal.DataTransferObjects.Positions
{
    public class AddEditCurrentPositionDto
    {
        public int ContractorId { get; set; }
        public DateTime? FromDate { get; set; }
        public int? DepartmentId { get; set; }
        public int? OrganizationRoleId { get; set; }
    }
}
