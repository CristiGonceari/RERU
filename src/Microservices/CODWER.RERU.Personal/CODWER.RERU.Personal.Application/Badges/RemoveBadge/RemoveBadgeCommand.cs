using CODWER.RERU.Personal.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Badges.RemoveBadge
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_BADGE)]

    public class RemoveBadgeCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
