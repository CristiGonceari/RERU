using CODWER.RERU.Personal.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Positions.RemovePosition
{
    [ModuleOperation(permission: PermissionCodes.POSITIONS_GENERAL_ACCESS)]

    public class RemovePositionCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
