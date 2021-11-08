using CODWER.RERU.Evaluation.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.Options.DeleteOption
{
    [ModuleOperation(permission: PermissionCodes.OPTIONS_GENERAL_ACCESS)]
    public class DeleteOptionCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
