﻿using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.EventEvaluators.GetEventEvaluators
{
    [ModuleOperation(permission: PermissionCodes.EVENT_EVALUATORS_GENERAL_ACCESS)]
    public class GetEventEvaluatorsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<UserProfileDto>>
    {
        public int EventId { get; set; }
    }
}
