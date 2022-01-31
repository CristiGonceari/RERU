using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.Tests.GetUserTestsByEvent
{
    public class GetUserTestsByEventQuery : PaginatedQueryParameter, IRequest<PaginatedModel<TestDto>>
    {
        public int UserId { get; set; }
        public int EventId { get; set; }
    }
}
