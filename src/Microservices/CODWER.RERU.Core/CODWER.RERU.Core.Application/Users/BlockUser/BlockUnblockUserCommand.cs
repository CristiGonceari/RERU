using CODWER.RERU.Core.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Core.Application.Users.BlockUser
{
    [ModuleOperation(permission: PermissionCodes.DEZACTIVAREA_UTILIZATORULUI)]
    public class BlockUnblockUserCommand : IRequest<Unit>
    {
        public BlockUnblockUserCommand(int id)
        {
            Id = id;
        }

        public int Id { set; get; }
    }
}
