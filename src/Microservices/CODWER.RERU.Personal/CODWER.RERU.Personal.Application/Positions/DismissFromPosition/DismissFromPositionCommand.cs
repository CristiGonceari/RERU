using CODWER.RERU.Personal.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Positions.DismissFromPosition
{
    [ModuleOperation(permission: PermissionCodes.POSITIONS_GENERAL_ACCESS)]

    public class DismissFromPositionCommand : IRequest<Unit>
    {
        public int ContractorId { get; set; }
    }
}
