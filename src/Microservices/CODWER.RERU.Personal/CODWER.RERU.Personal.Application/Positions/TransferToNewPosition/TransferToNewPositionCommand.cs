﻿using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.Positions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Positions.TransferToNewPosition
{
    [ModuleOperation(permission: PermissionCodes.POSITIONS_GENERAL_ACCESS)]

    public class TransferToNewPositionCommand : IRequest<Unit>
    {
        public AddEditCurrentPositionDto Data { get; set; }
    }
}