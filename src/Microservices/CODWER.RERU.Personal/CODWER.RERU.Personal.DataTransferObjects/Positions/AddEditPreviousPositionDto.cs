using System;

namespace CODWER.RERU.Personal.DataTransferObjects.Positions
{
    public class AddEditPreviousPositionDto
    {
        public int ContractorId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int? DepartmentId { get; set; }
        public int? OrganizationRoleId { get; set; }
    }
}
