using CODWER.RERU.Evaluation.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.PlanEvents.UnassignPlanEvent
{
    [ModuleOperation(permission: PermissionCodes.PLANS_GENERAL_ACCESS)]

    public class UnassignPlanEventCommand : IRequest<Unit>
    {
        public int PlanId { get; set; }
        public int EventId { get; set; }
    }
}
