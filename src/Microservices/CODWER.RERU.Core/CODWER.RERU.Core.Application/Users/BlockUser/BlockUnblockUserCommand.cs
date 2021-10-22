using CODWER.RERU.Core.Application.Module;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Core.Application.Users.BlockUser
{
    [ModuleOperation(permission: Permissions.DEACTIVATE_USER)]

    public class BlockUnblockUserCommand : IRequest<Unit>
    {
        public BlockUnblockUserCommand(int id)
        {
            Id = id;
        }
        public int Id { set; get; }
    }
}
