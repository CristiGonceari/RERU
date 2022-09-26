using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.Ranks;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Ranks.GetRanks
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_RANKURI)]

    public class GetRanksQuery : PaginatedQueryParameter, IRequest<PaginatedModel<RankDto>>
    {
        public int? ContractorId { get; set; }
    }
}
