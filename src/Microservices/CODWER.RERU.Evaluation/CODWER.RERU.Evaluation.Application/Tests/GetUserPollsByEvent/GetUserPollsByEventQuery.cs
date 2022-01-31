using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.Tests.GetUserPollsByEvent
{
    public class GetUserPollsByEventQuery : PaginatedQueryParameter, IRequest<PaginatedModel<PollDto>>
    {
        public int UserId { get; set; }
        public int EventId { get; set; }
    }
}
