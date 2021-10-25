using CODWER.RERU.Core.Application.Module;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Core.Application.Users.RemoveUser
{
    [ModuleOperation(permission: Permissions.DELETE_USER)]

    public class RemoveUserCommand : IRequest<Unit>
    {
        public RemoveUserCommand(int id)
        {
            Id = id;
        }
        public int Id { set; get; }
    }
}
