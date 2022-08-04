using CODWER.RERU.Evaluation.DataTransferObjects.SolicitedPositions;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.SolicitedPositions.UserSolicitedTests.GetUserSolicitedTests
{
    public  class GetUserSolicitedTestsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<SolicitedCandidatePositionDto>>
    {
        public int UserId { get; set; }
    }
}
