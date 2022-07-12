using CODWER.RERU.Core.DataTransferObjects.Modules;
using RERU.Data.Entities;
using System.Collections.Generic;

namespace CODWER.RERU.Core.DataTransferObjects.Users {
    public class ApplicationUserDto 
    {
        public string Id { get; set; }
        public string FirstName { set; get; }
        public string Email { set; get; }
        public string? MediaFileId { get; set; }
        public int DepartmentColaboratorId { get; set; }
        public int RoleColaboratorId { get; set; }
        public string[] Permissions { set; get; }
        public ApplicationUserModuleDto[] Modules { set; get; }
    }
}