using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.Tests.GetMyEvaluations
{
    public class GetMyEvaluationsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<TestDto>>
    {
        public string EvaluationName { get; set; }
        public string EvaluatedName { get; set; }
        public string EventName { get; set; }
    }
}
