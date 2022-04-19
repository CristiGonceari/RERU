using CODWER.RERU.Evaluation.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.PlanResponsiblePersons.UnassignPlanResponsiblePerson
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_PLANURI)]

    public class UnassignPlanResponsiblePersonCommand : IRequest<Unit>
    {
        public int PlanId { get; set; }
        public int UserProfileId { get; set; }
    }
}
