using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.PlanResponsiblePersons.GetPlanResponsiblePersons
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_PLANURI)]

    public class GetPlanResponsiblePersonsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<UserProfileDto>>
    {
        public int PlanId { get; set; }
    }
}
