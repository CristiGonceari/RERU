﻿using CODWER.RERU.Evaluation.DataTransferObjects.Locations;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.Locations.AddLocation
{
    [ModuleOperation(permission: Permissions.PermissionCodes.LOCATIONS_GENERAL_ACCESS )]
    public class AddLocationCommand : IRequest<int>
    {
        public AddEditLocationDto Data { get; set; }
    }
}
