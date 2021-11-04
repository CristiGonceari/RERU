using CODWER.RERU.Core.Application.Permissions;
using CVU.ERP.Common.Pagination;
using CODWER.RERU.Core.DataTransferObjects.Users;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Core.Application.Users.GetUsers 
{
    [ModuleOperation(permission: PermissionCodes.VIEW_ALL_USERS)]

    public class GetUsersQuery : PaginatedQueryParameter, IRequest<PaginatedModel<UserDto>> 
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Keyword { get; set; }
    }
}