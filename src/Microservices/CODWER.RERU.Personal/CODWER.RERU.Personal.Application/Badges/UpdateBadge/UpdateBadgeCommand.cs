using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.Badges;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Badges.UpdateBadge
{
    [ModuleOperation(permission: PermissionCodes.BADGES_GENERAL_ACCESS)]

    public class UpdateBadgeCommand : IRequest<Unit>
    {
        public AddEditBadgeDto Data { get; set; }
    }
}
