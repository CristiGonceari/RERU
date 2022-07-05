using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.Tests.GetMyEvaluations
{
    public class GetMyEvaluationsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<TestDto>>
    {
    }
}
