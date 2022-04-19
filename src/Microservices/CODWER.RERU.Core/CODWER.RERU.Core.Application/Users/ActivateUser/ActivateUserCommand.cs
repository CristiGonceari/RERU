using CODWER.RERU.Core.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Core.Application.Users.ActivateUser
{
    [ModuleOperation(permission: PermissionCodes.ACTIVAREA_UTILIZATORULUI)]
    public class ActivateUserCommand : IRequest<Unit>
    {
        public ActivateUserCommand(int id)
        {
            Id = id;
        }

        public int Id { set; get; }
    }
}
