using CODWER.RERU.Evaluation.DataTransferObjects.SolicitedTests;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.SolicitedPositions.UserSolicitedTests.GetUserSolicitedTests
{
    public  class GetUserSolicitedTestsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<SolicitedCandidatePositionDto>>
    {
        public int UserId { get; set; }
    }
}
