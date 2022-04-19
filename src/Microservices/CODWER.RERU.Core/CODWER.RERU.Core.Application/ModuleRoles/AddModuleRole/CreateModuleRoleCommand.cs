﻿using CODWER.RERU.Core.Application.Permissions;
using CODWER.RERU.Core.DataTransferObjects.ModuleRoles;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Core.Application.ModuleRoles.AddModuleRole
{
    [ModuleOperation(permission: PermissionCodes.ADĂUGAREA_ROLULUI_LA_MODUL)]

    public class CreateModuleRoleCommand : IRequest<Unit>
    {
        public CreateModuleRoleCommand(AddEditModuleRoleDto moduleRole)
        {
            ModuleRole = moduleRole;
        }

        public AddEditModuleRoleDto ModuleRole { set; get; }
    }
}