using CODWER.RERU.Core.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Core.Application.Roles.RemoveRole
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_ROLURI)]
    public class RemoveRoleCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
