using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.Tests.GetPollResult
{
    public class GetPollResultQuery : IRequest<PollResultDto>
    {
        public int TestTemplateId { get; set; }
        public int Index { get; set; }
    }
}
