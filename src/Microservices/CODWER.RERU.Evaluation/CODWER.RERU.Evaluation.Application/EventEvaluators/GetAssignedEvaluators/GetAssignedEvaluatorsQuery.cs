using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;
using CVU.ERP.Common.DataTransferObjects.Users;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.EventEvaluators.GetAssignedEvaluators
{
    public class GetAssignedEvaluatorsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<UserProfileDto>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string Email { get; set; }
        public string Idnp { get; set; }
        public int? DepartmentId { get; set; }
        public int? RoleId { get; set; }
        public UserStatusEnum? UserStatusEnum { get; set; }
        public int EventId { get; set; }

    }
}
