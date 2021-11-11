﻿using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.EventResponsiblePersons.GetEventResponsiblePersons
{
    [ModuleOperation(permission: PermissionCodes.EVENT_RESPONSIBLE_PERSONS_GENERAL_ACCESS)]
    public class GetEventResponsiblePersonsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<UserProfileDto>>
    {
        public int EventId { get; set; }
    }
}
