using CODWER.RERU.Personal.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Bonuses.RemoveBonus
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_BONUSURI)]

    public class RemoveBonusCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
