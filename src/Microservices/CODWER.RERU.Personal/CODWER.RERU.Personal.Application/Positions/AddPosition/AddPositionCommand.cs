using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.Positions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Positions.AddPosition
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_POZITII)]

    public class AddPositionCommand : IRequest<int>
    {
        public AddEditPositionDto Data { get; set; }
    }
}
