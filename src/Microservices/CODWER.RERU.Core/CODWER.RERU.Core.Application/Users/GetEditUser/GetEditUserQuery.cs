using CODWER.RERU.Core.Application.Module;
using CODWER.RERU.Core.DataTransferObjects.Users;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Core.Application.Users.GetEditUser {
    [ModuleOperation(permission: Permissions.EDIT_USER_DETAILS)]

    public class GetEditUserQuery : IRequest<UserDto> {
        public GetEditUserQuery (string id) {
            Id = Id;
        }
        public string Id { set; get; }
    }
}