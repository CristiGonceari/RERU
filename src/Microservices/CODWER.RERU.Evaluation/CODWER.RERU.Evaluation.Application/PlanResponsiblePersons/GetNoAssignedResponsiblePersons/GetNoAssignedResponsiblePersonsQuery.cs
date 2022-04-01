using CVU.ERP.Module.Application.Attributes;
using MediatR;
using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;
using CVU.ERP.Common.Pagination;

namespace CODWER.RERU.Evaluation.Application.PlanResponsiblePersons.GetNoAssignedResponsiblePersons
{
    [ModuleOperation(permission: PermissionCodes.PLANS_GENERAL_ACCESS)]
    public class GetNoAssignedResponsiblePersonsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<UserProfileDto>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Email { get; set; }
        public string Idnp { get; set; }
        public int PlanId { get; set; }
    }
}
