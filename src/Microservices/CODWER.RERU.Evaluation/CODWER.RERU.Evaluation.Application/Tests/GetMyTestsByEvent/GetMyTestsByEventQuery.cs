using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.Tests.GetMyTestsByEvent
{
    public class GetMyTestsByEventQuery : PaginatedQueryParameter, IRequest<PaginatedModel<TestDto>>
    {
        public int EventId { get; set; }
    }
}
