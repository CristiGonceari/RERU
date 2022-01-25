using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.Bonuses;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Bonuses.AddBonus
{
    [ModuleOperation(permission: PermissionCodes.BONUSES_GENERAL_ACCESS)]

    public class AddBonusCommand : IRequest<int>
    {
        public AddEditBonusDto Data { get; set; }
    }
}
