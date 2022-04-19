using CODWER.RERU.Core.Application.Permissions;
using CODWER.RERU.Core.DataTransferObjects.Password;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Core.Application.Users.SetPassword 
{
    [ModuleOperation(permission: PermissionCodes.SETAREA_PAROLEI_UTILIZATORULUI)]

    public class SetPasswordCommand : IRequest<Unit> 
    {
        public SetPasswordCommand (SetPasswordDto setPassword) 
        {
            Data = setPassword;
        }

        public SetPasswordDto Data { set; get; }
    }
}