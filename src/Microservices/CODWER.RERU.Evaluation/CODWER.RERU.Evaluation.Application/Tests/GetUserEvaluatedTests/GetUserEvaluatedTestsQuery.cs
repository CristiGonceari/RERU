using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.Tests.GetUserEvaluatedTests
{
    public class GetUserEvaluatedTestsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<TestDto>>
    {
        public int UserId { get; set; }
    }
}
