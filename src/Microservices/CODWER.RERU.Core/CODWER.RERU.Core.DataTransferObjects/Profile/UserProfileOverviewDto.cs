using System.Collections.Generic;
using CVU.ERP.Common.DataTransferObjects.Users;

namespace CODWER.RERU.Core.DataTransferObjects.Profile 
{
    public class UserProfileOverviewDto 
    {
        public string Email { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string FatherName { set; get; }
        public string Idnp { set; get; }
        public string? MediaFileId { set; get; }
        public int? DepartmentColaboratorId { get; set; }
        public int? RoleColaboratorId { get; set; }
        public UserStatusEnum? UserStatusEnum { get; set; }
        public IEnumerable<UserProfileModuleRowDto> Modules { set; get; }
    }
}