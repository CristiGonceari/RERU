using MediatR;

namespace CODWER.RERU.Evaluation.Application.Tests.AddMyPoll
{
    public class AddMyPollCommand : IRequest<int>
    {
        public int TestTypeId { get; set; }
        public int EventId { get; set; }
    }
}
