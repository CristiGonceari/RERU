using CODWER.RERU.Personal.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Badges.RemoveBadge
{
    [ModuleOperation(permission: PermissionCodes.BADGES_GENERAL_ACCESS)]

    public class RemoveBadgeCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
