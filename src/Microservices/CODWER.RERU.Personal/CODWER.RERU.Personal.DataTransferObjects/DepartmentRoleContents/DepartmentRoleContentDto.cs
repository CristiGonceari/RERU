using System.Collections.Generic;

namespace CODWER.RERU.Personal.DataTransferObjects.DepartmentRoleContents
{
    public class DepartmentRoleContentDto
    {
        public DepartmentRoleContentDto()
        {
            Roles = new List<RoleFromDepartment>();
        }

        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public List<RoleFromDepartment> Roles { get; set; }
    }
}
