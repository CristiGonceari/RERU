using System.Collections.Generic;
using CODWER.RERU.Evaluation.DataTransferObjects.CandidatePositions;
using CVU.ERP.Common.DataTransferObjects.Users;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles
{
    public class UserProfileDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string Email { get; set; }
        public string Idnp { get; set; }
        public int? DepartmentColaboratorId { get; set; }
        public string DepartmentName { get; set; }
        public int? RoleColaboratorId { get; set; }
        public string RoleName { get; set; }
        public UserStatusEnum? UserStatusEnum { get; set; }
        public AccessModeEnum? AccessModeEnum { get; set; }
        public IEnumerable<string> Permissions { get; set; }
        public List<string> CandidatePositionNames { get; set; }

        public string FullName => $"{FirstName} {LastName} {FatherName}";
    }
}
