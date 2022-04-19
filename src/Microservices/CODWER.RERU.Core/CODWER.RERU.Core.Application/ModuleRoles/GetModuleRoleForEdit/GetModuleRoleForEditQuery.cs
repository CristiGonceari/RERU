using CODWER.RERU.Core.Application.Permissions;
using CODWER.RERU.Core.DataTransferObjects.ModuleRoles;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Core.Application.ModuleRoles.GetModuleRoleForEdit
{
    [ModuleOperation(permission: PermissionCodes.ACTUALIZAREA_ROLULUI_LA_MODUL)]

    public class GetModuleRoleForEditQuery : IRequest<AddEditModuleRoleDto>
    {
        public GetModuleRoleForEditQuery(int id)
        {
            Id = id;
        }
        public int Id { set; get; }
    }
}