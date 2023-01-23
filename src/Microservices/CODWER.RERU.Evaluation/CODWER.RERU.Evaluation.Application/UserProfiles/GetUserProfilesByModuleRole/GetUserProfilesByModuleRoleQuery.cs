using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;
using CVU.ERP.Common.DataTransferObjects.Users;
using CVU.ERP.Common.Pagination;
using MediatR;
using System.Collections.Generic;

namespace CODWER.RERU.Evaluation.Application.UserProfiles.GetUserProfilesByModuleRole
{
    public class GetUserProfilesByModuleRoleQuery : PaginatedQueryParameter, IRequest<PaginatedModel<UserProfileDto>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string Email { get; set; }
        public string Idnp { get; set; }
        public int? DepartmentId { get; set; }
        public int? RoleId { get; set; }
        public int? FunctionId { get; set; }
        public UserStatusEnum? UserStatusEnum { get; set; }

        public int TestTemplateId { get; set; }

        public bool EventUsers { get; set; }
        public bool EventResponsiblePerson { get; set; }

        public List<int> ExceptUserIds { get; set; }
    }
}
