using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using CVU.ERP.Common.Pagination;
using MediatR;
using System;

namespace CODWER.RERU.Evaluation.Application.Tests.MyActivities.GetMyEvaluatedTests.GetMyEvaluatedTestsByDate
{
    public class GetMyEvaluatedTestsByDateQuery : PaginatedQueryParameter, IRequest<PaginatedModel<TestDto>>
    {
        public DateTime Date { get; set; }
    }
}
