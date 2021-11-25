using CODWER.RERU.Evaluation.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.PlanResponsiblePersons.UnassignPlanResponsiblePerson
{
    [ModuleOperation(permission: PermissionCodes.PLANS_GENERAL_ACCESS)]

    public class UnassignPlanResponsiblePersonCommand : IRequest<Unit>
    {
        public int PlanId { get; set; }
        public int UserProfileId { get; set; }
    }
}
