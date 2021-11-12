using MediatR;
using System.Collections.Generic;
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;

namespace CODWER.RERU.Evaluation.Application.Tests.GetMyPollsByEvent
{
    public class GetMyPollsByEventQuery : IRequest<List<PollDto>>
    {
        public int EventId { get; set; }
    }
}
