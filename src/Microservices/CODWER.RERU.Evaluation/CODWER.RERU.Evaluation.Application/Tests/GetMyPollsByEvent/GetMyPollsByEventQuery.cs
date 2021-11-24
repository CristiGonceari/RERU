using MediatR;
using System.Collections.Generic;
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using CVU.ERP.Common.Pagination;

namespace CODWER.RERU.Evaluation.Application.Tests.GetMyPollsByEvent
{
    public class GetMyPollsByEventQuery : PaginatedQueryParameter, IRequest<PaginatedModel<PollDto>>
    {
        public int EventId { get; set; }
    }
}
