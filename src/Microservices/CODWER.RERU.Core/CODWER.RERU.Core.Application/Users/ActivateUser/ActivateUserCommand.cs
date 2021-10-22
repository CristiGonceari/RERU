using CODWER.RERU.Core.Application.Module;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Core.Application.Users.ActivateUser
{
    [ModuleOperation(permission: Permissions.ACTIVATE_USER)]

    public class ActivateUserCommand : IRequest<Unit>
    {
        public ActivateUserCommand(int id)
        {
            Id = id;
        }
        public int Id { set; get; }
    }
}
