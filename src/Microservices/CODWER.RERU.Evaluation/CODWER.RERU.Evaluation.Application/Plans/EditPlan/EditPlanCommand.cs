using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.Plans;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.Plans.EditPlan
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_PLANURI)]

    public class EditPlanCommand : IRequest<int>
    {
        public AddEditPlanDto Data { get; set; }
    }

}
