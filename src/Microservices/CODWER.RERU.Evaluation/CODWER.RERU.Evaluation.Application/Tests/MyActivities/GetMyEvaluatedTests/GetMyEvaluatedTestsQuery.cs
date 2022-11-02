using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using CVU.ERP.Common.Pagination;
using MediatR;
using System;

namespace CODWER.RERU.Evaluation.Application.Tests.MyActivities.GetMyEvaluatedTests
{
    public class GetMyEvaluatedTestsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<TestDto>>
    {
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}
