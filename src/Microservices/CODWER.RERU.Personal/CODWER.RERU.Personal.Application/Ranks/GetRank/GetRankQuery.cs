using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.Ranks;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Ranks.GetRank
{
    [ModuleOperation(permission: PermissionCodes.ACCESS_GENERAL_LA_RANKURI)]

    public class GetRankQuery : IRequest<RankDto>
    {
        public int Id { get; set; }
    }
}
