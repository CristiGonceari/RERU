using System;
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.Tests.MyActivities.GetMyTests
{
    public class GetMyTestsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<TestDto>>
    {
        public DateTime Date { get; set; }
        public string TestName { get; set; }
        public string EventName { get; set; }
    }
}
