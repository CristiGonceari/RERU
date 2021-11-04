using CODWER.RERU.Core.Application.Permissions;
using CODWER.RERU.Core.DataTransferObjects.Users;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Core.Application.Users.GetEditUser 
{
    [ModuleOperation(permission: PermissionCodes.EDIT_USER_DETAILS)]

    public class GetEditUserQuery : IRequest<UserDto> 
    {
        public GetEditUserQuery (string id) {
            Id = id;
        }

        public string Id { set; get; }
    }
}