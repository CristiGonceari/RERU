using CODWER.RERU.Core.DataTransferObjects.ModuleRoles;
using CVU.ERP.Module.Application.Attributes;
using MediatR;
using System.Collections.Generic;
using CODWER.RERU.Core.Application.Permissions;

namespace CODWER.RERU.Core.Application.ModuleRolePermissions.GetModuleRolePermissionsForUpdate
{
    public class GetModuleRolePermissionsForUpdateQuery :  IRequest<List<ModuleRolePermissionGrantedRowDto>>
    {
        [ModuleOperation(permission: PermissionCodes.UPDATE_MODULE_ROLE_PERMISSIONS)]

        public GetModuleRolePermissionsForUpdateQuery(int id)
        {
            RoleId = id;
        }
        public int RoleId { get; set; }
    }
}