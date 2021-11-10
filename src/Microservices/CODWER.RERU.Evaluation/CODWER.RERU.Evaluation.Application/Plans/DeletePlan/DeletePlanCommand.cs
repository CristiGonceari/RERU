using CODWER.RERU.Evaluation.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.Plans.DeletePlan
{
    [ModuleOperation(permission: PermissionCodes.PLANS_GENERAL_ACCESS)]

    public class DeletePlanCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }

}
