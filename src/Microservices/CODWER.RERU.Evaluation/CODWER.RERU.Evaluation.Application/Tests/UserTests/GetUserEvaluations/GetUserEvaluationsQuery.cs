using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using CVU.ERP.Common.Pagination;
using MediatR;
using System;

namespace CODWER.RERU.Evaluation.Application.Tests.UserTests.GetUserEvaluations
{
    public class GetUserEvaluationsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<TestDto>>
    {
        public int UserId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
