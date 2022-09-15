using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using CVU.ERP.Common.Pagination;
using MediatR;
using System;

namespace CODWER.RERU.Evaluation.Application.Tests.GetMyEvaluations
{
    public class GetMyEvaluationsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<TestDto>>
    {
        public string EvaluatedName { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
