using CODWER.RERU.Core.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Core.Application.Roles.AddRole
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_ROLURI)]
    public class AddRoleCommand : IRequest<int>
    {
        public string Name { get; set; }
        public int ColaboratorId { get; set; }
    }
}
