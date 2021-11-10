using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.Plans;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.Plans.AddPlan
{
    [ModuleOperation(permission: PermissionCodes.PLANS_GENERAL_ACCESS)]

    public class AddPlanCommand : IRequest<int>
    {
        public AddEditPlanDto Data { get; set; }
    }
}
