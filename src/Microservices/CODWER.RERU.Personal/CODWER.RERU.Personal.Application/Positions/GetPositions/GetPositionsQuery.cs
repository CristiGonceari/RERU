using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.Positions;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Positions.GetPositions
{
    [ModuleOperation(permission: PermissionCodes.POSITIONS_GENERAL_ACCESS)]

    public class GetPositionsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<PositionDto>>
    {
        public int? DepartmentId { get; set; }
        public int? RoleId { get; set; }
        public int? ContractorId { get; set; }
    }
}
