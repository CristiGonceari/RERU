using CODWER.RERU.Core.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Core.Application.Roles.UpdateRole
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_DEPARTAMENTE)]
    public class UpdateRoleCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ColaboratorId { get; set; }
    }
}
