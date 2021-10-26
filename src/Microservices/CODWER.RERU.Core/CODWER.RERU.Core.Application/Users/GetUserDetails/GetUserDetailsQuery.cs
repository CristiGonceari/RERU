using CODWER.RERU.Core.Application.Permissions;
using CODWER.RERU.Core.DataTransferObjects.Users;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Core.Application.Users.GetUserDetails 
{
    [ModuleOperation(permission: PermissionCodes.VIEW_USER_DETAILS)]

    public class GetUserDetailsQuery : IRequest<UserDetailsOverviewDto> 
    {
        public GetUserDetailsQuery (int id) {
            Id = id;
        }

        public int Id { set; get; }
    }
}