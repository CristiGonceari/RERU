﻿using CVU.ERP.Module.Application.Attributes;
using MediatR;
using System.Collections.Generic;

namespace CODWER.RERU.Evaluation.Application.LocationResponsiblePersons.AssignResponsiblePersonToLocation
{
    [ModuleOperation(permission: Permissions.PermissionCodes.LOCATIONS_GENERAL_ACCESS)]
    public class AssignResponsiblePersonToLocationCommand : IRequest<List<int>>
    {
        public int LocationId { get; set; }
        public List<int> UserProfileId { get; set; }

    }
}
