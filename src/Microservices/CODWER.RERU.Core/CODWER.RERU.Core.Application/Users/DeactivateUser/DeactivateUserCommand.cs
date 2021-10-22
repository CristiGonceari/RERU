using CODWER.RERU.Core.Application.Module;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Core.Application.Users.DeactivateUser
{
    [ModuleOperation(permission: Permissions.DEACTIVATE_USER)]

    public class DeactivateUserCommand : IRequest<Unit>
    {
        public DeactivateUserCommand(int id)
        {
            Id = id;
        }
        public int Id { set; get; }
    }
}
