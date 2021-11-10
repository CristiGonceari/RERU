using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.Plans;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.PlanEvents.UnassignPlanEvent
{
    [ModuleOperation(permission: PermissionCodes.PLAN_EVENTS_GENERAL_ACCESS)]

    public class UnassignPlanEventCommand : IRequest<Unit>
    {
        public AddPlanEventDto Data { get; set; }
    }
}
