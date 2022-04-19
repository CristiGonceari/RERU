using CODWER.RERU.Core.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Core.Application.Users.RemoveUser
{
    [ModuleOperation(permission: PermissionCodes.STERGEREA_UTILIZATORULUI)]
    public class RemoveUserCommand : IRequest<Unit>
    {
        public RemoveUserCommand(int id)
        {
            Id = id;
        }

        public int Id { set; get; }
    }
}
