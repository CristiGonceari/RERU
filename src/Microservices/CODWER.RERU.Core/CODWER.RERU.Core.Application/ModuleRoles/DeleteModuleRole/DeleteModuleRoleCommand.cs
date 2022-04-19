using CODWER.RERU.Core.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Core.Application.ModuleRoles.DeleteModuleRole
{
    [ModuleOperation(permission: PermissionCodes.STERGEREA_ROLULUI_LA_MODUL)]

    public class DeleteModuleRoleCommand : IRequest<Unit>
    {
        public DeleteModuleRoleCommand(int id)
        {
            Id = id;
        }
        public int Id { set; get; }
    }
}
