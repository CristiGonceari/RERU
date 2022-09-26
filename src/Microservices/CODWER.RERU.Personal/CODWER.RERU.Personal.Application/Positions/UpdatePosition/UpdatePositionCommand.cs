using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.Positions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Positions.UpdatePosition
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_POZITII)]

    public class UpdatePositionCommand : IRequest<Unit>
    {
        public AddEditPositionDto Data { get; set; }
    }
}
