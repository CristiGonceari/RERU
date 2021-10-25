
using CODWER.RERU.Core.Application.Module;
using CODWER.RERU.Core.DataTransferObjects.ModuleRoles;
using CVU.ERP.Module.Application.Attributes;
using MediatR;
using System.Collections.Generic;

namespace CODWER.RERU.Core.Application.ModuleRolePermissions.GetModuleRolePermissionsForUpdate
{
    public class GetModuleRolePermissionsForUpdateQuery :  IRequest<List<ModuleRolePermissionGrantedRowDto>>
    {
        [ModuleOperation(permission: Permissions.UPDATE_MODULE_ROLE_PERMISSIONS)]

        public GetModuleRolePermissionsForUpdateQuery(int id)
        {
            RoleId = id;
        }
        public int RoleId { get; set; }
    }
}