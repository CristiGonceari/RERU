using CVU.ERP.Module.Application.Attributes;
using MediatR;
using System.Collections.Generic;
using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;

namespace CODWER.RERU.Evaluation.Application.EventUsers.GetNoAssignedUsers
{
    [ModuleOperation(permission: PermissionCodes.EVENT_USERS_GENERAL_ACCESS)]
    public class GetNoAssignedUsersQuery : IRequest<List<UserProfileDto>>
    {
        public int EventId { get; set; }
        public string Keyword { get; set; }
    }
}
