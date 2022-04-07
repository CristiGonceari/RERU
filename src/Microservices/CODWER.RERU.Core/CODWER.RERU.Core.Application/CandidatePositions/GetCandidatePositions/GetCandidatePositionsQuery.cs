using CODWER.RERU.Core.DataTransferObjects.CandidatePositions;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Core.Application.CandidatePositions.GetCandidatePositions
{
    public class GetCandidatePositionsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<CandidatePositionDto>>
    {
        public string Name { get; set; }
    }
}
