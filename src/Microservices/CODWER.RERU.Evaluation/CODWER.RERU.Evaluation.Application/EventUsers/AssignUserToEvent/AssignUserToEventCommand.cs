﻿using CODWER.RERU.Evaluation.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;
using System.Collections.Generic;

namespace CODWER.RERU.Evaluation.Application.EventUsers.AssignUserToEvent
{
    [ModuleOperation(permission: PermissionCodes.EVENTS_GENERAL_ACCESS)]
    public class AssignUserToEventCommand : IRequest<List<int>>
    {
        public int EventId { get; set; }
        public List<int> UserProfileId { get; set; }
    }
}
