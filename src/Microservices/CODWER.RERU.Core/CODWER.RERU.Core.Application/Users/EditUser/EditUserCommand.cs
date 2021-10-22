using CODWER.RERU.Core.Application.Module;
using CODWER.RERU.Core.DataTransferObjects.Users;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Core.Application.Users.EditUser {
    [ModuleOperation(permission: Permissions.EDIT_USER_DETAILS)]

    public class EditUserCommand : IRequest<Unit> {
        public EditUserCommand(EditUserDto user)
        {
            User = user;
        }
        public EditUserDto User { set; get; }
    }
}