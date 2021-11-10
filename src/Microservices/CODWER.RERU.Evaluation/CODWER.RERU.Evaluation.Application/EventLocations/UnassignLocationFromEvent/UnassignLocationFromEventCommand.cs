﻿using CODWER.RERU.Evaluation.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.EventLocations.UnassignLocationFromEvent
{
    [ModuleOperation(permission: PermissionCodes.EVENT_LOCATIONS_GENERAL_ACCESS)]
    public class UnassignLocationFromEventCommand : IRequest<Unit>
    {
        public int EventId { get; set; }
        public int LocationId { get; set; }
    }
}
