using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.Ranks;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Ranks.UpdateRank
{
    [ModuleOperation(permission: PermissionCodes.RANKS_GENERAL_ACCESS)]

    public class UpdateRankCommand : IRequest<Unit>
    {
        public AddEditRankDto Data { get; set; }
    }
}
