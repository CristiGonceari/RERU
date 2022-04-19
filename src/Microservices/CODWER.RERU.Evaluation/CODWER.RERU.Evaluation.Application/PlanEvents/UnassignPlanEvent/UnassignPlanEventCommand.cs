using CODWER.RERU.Evaluation.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.PlanEvents.UnassignPlanEvent
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_PLANURI)]

    public class UnassignPlanEventCommand : IRequest<Unit>
    {
        public int PlanId { get; set; }
        public int EventId { get; set; }
    }
}
