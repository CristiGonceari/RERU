using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.Plans;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.PlanResponsiblePersons.AssignResponsiblePersonToPlan
{
    [ModuleOperation(permission: PermissionCodes.PLAN_RESPONSIBLE_PERSONS_GENERAL_ACCESS)]

    public class AssignResponsiblePersonToPlanCommand : IRequest<Unit>
    {
        public AddPlanPersonDto Data { get; set; }
    }
}
