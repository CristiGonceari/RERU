using CODWER.RERU.Personal.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Ranks.RemoveRank
{
    [ModuleOperation(permission: PermissionCodes.RANKS_GENERAL_ACCESS)]

    public class RemoveRankCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
