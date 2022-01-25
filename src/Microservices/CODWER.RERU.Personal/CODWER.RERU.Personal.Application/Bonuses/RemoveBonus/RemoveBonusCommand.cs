using CODWER.RERU.Personal.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Bonuses.RemoveBonus
{
    [ModuleOperation(permission: PermissionCodes.BONUSES_GENERAL_ACCESS)]

    public class RemoveBonusCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
