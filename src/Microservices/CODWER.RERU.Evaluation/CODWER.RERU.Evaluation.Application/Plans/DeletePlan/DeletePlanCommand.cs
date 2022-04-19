using CODWER.RERU.Evaluation.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.Plans.DeletePlan
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_PLANURI)]

    public class DeletePlanCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }

}
