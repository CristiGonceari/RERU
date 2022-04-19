using CODWER.RERU.Core.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Core.Application.Users.ResetUserPassword 
{
    [ModuleOperation(permission: PermissionCodes.RESETAREA_PAROLEI_UTILIZATORULUI)]

    public class ResetUserPasswordCommand : IRequest<Unit>
    {
        public ResetUserPasswordCommand (int id) 
        {
            Id = id;
        }

        public int Id { set; get; }
    }
}