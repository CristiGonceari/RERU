using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.Bonuses;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Bonuses.UpdateBonus
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_BONUSURI)]

    public class UpdateBonusCommand : IRequest<Unit>
    {
        public AddEditBonusDto Data { get; set; }
    }
}
