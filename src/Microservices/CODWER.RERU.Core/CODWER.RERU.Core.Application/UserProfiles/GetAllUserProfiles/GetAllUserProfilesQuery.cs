using CODWER.RERU.Core.Application.Permissions;
using CVU.ERP.Common.Pagination;
using CODWER.RERU.Core.DataTransferObjects.UserProfiles;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Core.Application.UserProfiles.GetAllUserProfiles
{
    [ModuleOperation(permission: PermissionCodes.VIEW_ALL_USERS)]
    public class GetAllUserProfilesQuery : PaginatedQueryParameter, IRequest<PaginatedModel<UserProfileDto>>
    {
        public string Keyword { get; set; }
        public string Order { get; set; }
        public string Sort { get; set; }
        public bool? Status { get; set; }

    }
}