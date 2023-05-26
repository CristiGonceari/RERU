using System;
using CVU.ERP.Common.DataTransferObjects.Users;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Core.DataTransferObjects.UserProfiles
{
    public class UserProfileDto
    {
        public string Id { get; set; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string FatherName { set; get; }
        public string UserName { set; get; }
        public string Email { get; set; }
        public string Idnp { get; set; }
        public string PhoneNumber { get; set; }
        public string Birthday { get; set; }
        public string? MediaFileId { get; set; }
        public int? CandidatePositionId { set; get; }
        public int? DepartmentColaboratorId { get; set; }
        public string DepartmentName { get; set; }
        public int? RoleColaboratorId { get; set; }
        public string RoleName { get; set; }
        public int? FunctionColaboratorId { get; set; }
        public string FunctionName { get; set; }
        public UserStatusEnum? UserStatusEnum { get; set; }
        public AccessModeEnum AccessModeEnum { get; set; }
        public bool IsActive { set; get; }
    }
}
