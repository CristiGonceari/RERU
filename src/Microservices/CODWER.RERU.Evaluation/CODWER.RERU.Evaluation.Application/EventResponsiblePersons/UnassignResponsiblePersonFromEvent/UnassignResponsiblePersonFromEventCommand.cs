﻿using CODWER.RERU.Evaluation.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.EventResponsiblePersons.UnassignResponsiblePersonFromEvent
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_EVENIMENTE)]
    public class UnassignResponsiblePersonFromEventCommand : IRequest<Unit>
    {
        public int EventId { get; set; }
        public int UserProfileId { get; set; }
    }
}
