using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.PlanEvents.GetPlanEvents
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_PLANURI)]
    public class GetPlanEventsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<EventDto>>
    {
        public int PlanId { get; set; }
    }
}
