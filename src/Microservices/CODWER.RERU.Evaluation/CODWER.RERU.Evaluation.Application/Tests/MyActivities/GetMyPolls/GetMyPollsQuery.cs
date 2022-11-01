using System;
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.Tests.MyActivities.GetMyPolls
{
    public class GetMyPollsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<PollDto>>
    {
        public DateTime Date { get; set; }
    }
}
