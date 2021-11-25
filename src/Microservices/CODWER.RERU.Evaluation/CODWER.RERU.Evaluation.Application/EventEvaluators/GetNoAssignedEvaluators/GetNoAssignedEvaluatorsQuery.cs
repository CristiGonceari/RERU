using MediatR;
using System.Collections.Generic;
using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;
using CVU.ERP.Module.Application.Attributes;

namespace CODWER.RERU.Evaluation.Application.EventEvaluators.GetNoAssignedEvaluators
{
    [ModuleOperation(permission: PermissionCodes.EVENTS_GENERAL_ACCESS)]
    public class GetNoAssignedEvaluatorsQuery : IRequest<List<UserProfileDto>>
    {
        public int EventId { get; set; }
        public string Keyword { get; set; }
    }
}
