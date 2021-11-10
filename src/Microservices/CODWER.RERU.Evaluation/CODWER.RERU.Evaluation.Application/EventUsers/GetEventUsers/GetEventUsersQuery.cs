using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.EventUsers.GetEventUsers
{
    [ModuleOperation(permission: PermissionCodes.EVENT_USERS_GENERAL_ACCESS)]
    public class GetEventUsersQuery : PaginatedQueryParameter, IRequest<PaginatedModel<UserProfileDto>>
    {
        public int EventId { get; set; }
    }
}
