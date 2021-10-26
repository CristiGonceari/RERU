using CODWER.RERU.Core.Application.Permissions;
using CODWER.RERU.Core.DataTransferObjects.Users;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Core.Application.Users.EditUser 
{
    [ModuleOperation(permission: PermissionCodes.EDIT_USER_DETAILS)]

    public class EditUserCommand : IRequest<Unit> 
    {
        public EditUserCommand(EditUserDto user)
        {
            User = user;
        }

        public EditUserDto User { set; get; }
    }
}