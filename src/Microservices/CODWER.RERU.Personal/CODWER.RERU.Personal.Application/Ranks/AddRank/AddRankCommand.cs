using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.Ranks;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Ranks.AddRank
{
    [ModuleOperation(permission: PermissionCodes.RANKS_GENERAL_ACCESS)]

    public class AddRankCommand : IRequest<int>
    {
        public AddEditRankDto Data { get; set; }
    }
}
