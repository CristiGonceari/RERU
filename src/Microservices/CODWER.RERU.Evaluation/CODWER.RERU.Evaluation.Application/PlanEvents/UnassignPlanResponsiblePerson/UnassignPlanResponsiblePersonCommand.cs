using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.Plans;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.PlanEvents.UnassignPlanResponsiblePerson
{
    [ModuleOperation(permission: PermissionCodes.PLAN_RESPONSIBLE_PERSONS_GENERAL_ACCESS)]

    public class UnassignPlanResponsiblePersonCommand : IRequest<Unit>
    {
        public AddPlanPersonDto Data { get; set; }
    }
}
