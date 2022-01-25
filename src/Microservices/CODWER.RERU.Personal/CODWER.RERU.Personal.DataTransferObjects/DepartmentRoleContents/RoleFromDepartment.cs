using System.Collections.Generic;
using CVU.ERP.Common.DataTransferObjects.SelectValues;

namespace CODWER.RERU.Personal.DataTransferObjects.DepartmentRoleContents
{
    public class RoleFromDepartment
    {
        public RoleFromDepartment()
        {
            ContractorIds = new List<SelectItem>();
        }
        public int OrganizationRoleId { get; set; }
        public string OrganizationRoleName { get; set; }
        public int OrganizationRoleCount { get; set; }
        public List<SelectItem> ContractorIds { get; set; }

        public int? DepartmentRoleContentId { get; set; }
    }
}
