using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.Bonuses;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Bonuses.GetBonus
{
    [ModuleOperation(permission: PermissionCodes.BONUSES_GENERAL_ACCESS)]

    public class GetBonusQuery : IRequest<BonusDto>
    {
        public int Id { get; set; }
    }
}
