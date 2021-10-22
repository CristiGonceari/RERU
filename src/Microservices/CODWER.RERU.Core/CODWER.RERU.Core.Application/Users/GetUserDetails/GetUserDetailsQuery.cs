using CODWER.RERU.Core.Application.Module;
using CODWER.RERU.Core.DataTransferObjects.Users;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Core.Application.Users.GetUserDetails {
    [ModuleOperation(permission: Permissions.VIEW_USER_DETAILS)]

    public class GetUserDetailsQuery : IRequest<UserDetailsOverviewDto> {
        public GetUserDetailsQuery (int id) {
            Id = id;
        }
        public int Id { set; get; }
    }
}