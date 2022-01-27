using CODWER.RERU.Personal.DataTransferObjects.Ranks;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Personal.Application.Profiles.ContractorRanks.GetContractorRanks
{
    public class GetContractorRanksQuery : PaginatedQueryParameter, IRequest<PaginatedModel<RankDto>>
    {
    }
}
