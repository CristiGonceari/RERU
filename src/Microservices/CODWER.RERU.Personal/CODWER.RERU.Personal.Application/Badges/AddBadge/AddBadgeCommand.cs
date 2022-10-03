using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.Badges;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Badges.AddBadge
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_BADGE)]

    public class AddBadgeCommand : IRequest<int>
    {
        public AddEditBadgeDto Data { get; set; }
    }
}
