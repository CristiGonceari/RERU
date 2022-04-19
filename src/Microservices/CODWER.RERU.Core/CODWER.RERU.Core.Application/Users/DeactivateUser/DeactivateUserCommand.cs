﻿using CODWER.RERU.Core.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Core.Application.Users.DeactivateUser
{
    [ModuleOperation(permission: PermissionCodes.DEZACTIVAREA_UTILIZATORULUI)]
    public class DeactivateUserCommand : IRequest<Unit>
    {
        public DeactivateUserCommand(int id)
        {
            Id = id;
        }

        public int Id { set; get; }
    }
}
