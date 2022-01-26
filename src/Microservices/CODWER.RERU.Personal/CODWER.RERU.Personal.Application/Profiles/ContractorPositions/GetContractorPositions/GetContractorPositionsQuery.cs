using CODWER.RERU.Personal.DataTransferObjects.Positions;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Personal.Application.Profiles.ContractorPositions.GetContractorPositions
{
    public class GetContractorPositionsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<PositionDto>>
    {
    }
}
