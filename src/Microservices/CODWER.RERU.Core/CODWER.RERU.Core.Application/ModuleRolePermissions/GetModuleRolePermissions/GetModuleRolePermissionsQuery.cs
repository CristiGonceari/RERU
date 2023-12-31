﻿
using CVU.ERP.Common.Pagination;
using CODWER.RERU.Core.Application.Module;
using CODWER.RERU.Core.DataTransferObjects.ModulePermissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Core.Application.ModuleRolePermissions.GetModuleRolePermissions
{
    [ModuleOperation(permission: Permissions.VIEW_ROLE_PERMISSIONS)]

    public class GetModuleRolePermissionsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<ModulePermissionRowDto>>
    {
        public int RoleId { get; set; }
    }
}