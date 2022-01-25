using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.Positions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Positions.GetCurrentContractorPosition
{
    [ModuleOperation(permission: PermissionCodes.POSITIONS_GENERAL_ACCESS)]

    public class GetCurrentContractorPositionQuery : IRequest<CurrentPositionDto>
    {
        public int ContractorId { get; set; }
    }
}
