using CODWER.RERU.Evaluation.DataTransferObjects.SolicitedTests;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.SolicitedPositions.MySolicitedPositions.GetMySolicitedTests
{
    public class GetMySolicitedPositionQuery : PaginatedQueryParameter, IRequest<PaginatedModel<SolicitedCandidatePositionDto>>
    {
    }
}
