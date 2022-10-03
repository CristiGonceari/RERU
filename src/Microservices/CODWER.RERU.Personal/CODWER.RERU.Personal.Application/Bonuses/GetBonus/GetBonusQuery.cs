using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.Bonuses;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Bonuses.GetBonus
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_BONUSURI)]

    public class GetBonusQuery : IRequest<BonusDto>
    {
        public int Id { get; set; }
    }
}
