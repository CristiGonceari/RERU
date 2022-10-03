using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.Badges;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Badges.GetBadges
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_BADGE)]

    public class GetBadgesQuery : PaginatedQueryParameter, IRequest<PaginatedModel<BadgeDto>>
    {
        public int? ContractorId { get; set; }
    }
}
