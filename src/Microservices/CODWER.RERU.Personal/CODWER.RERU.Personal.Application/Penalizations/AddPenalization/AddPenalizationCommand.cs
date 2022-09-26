﻿using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.Penalizations;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Penalizations.AddPenalization
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_PENALIZARI)]

    public class AddPenalizationCommand : IRequest<int>
    {
        public AddEditPenalizationDto Data { get; set; }
    }
}
