using System.Collections.Generic;

namespace CODWER.RERU.Personal.DataTransferObjects.DepartmentRoleRelations.Get
{
    public class DepartmentRoleRelationDto
    {
        public List<DepartmentRelationDto> Departments { get; set; }
        public List<RoleRelationDto> Roles { get; set; }

    }
}
