using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.Badges;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Badges.GetBadge
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_BADGE)]

    public class GetBadgeQuery : IRequest<BadgeDto>
    {
        public int Id { get; set; }
    }
}
