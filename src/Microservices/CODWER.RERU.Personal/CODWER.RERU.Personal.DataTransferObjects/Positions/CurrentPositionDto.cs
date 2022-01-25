using System;

namespace CODWER.RERU.Personal.DataTransferObjects.Positions
{
    public class CurrentPositionDto
    {
        public DateTime? FromDate { get; set; }
        public int? DepartmentId { get; set; }
        public int? OrganizationRoleId { get; set; }
    }
}
