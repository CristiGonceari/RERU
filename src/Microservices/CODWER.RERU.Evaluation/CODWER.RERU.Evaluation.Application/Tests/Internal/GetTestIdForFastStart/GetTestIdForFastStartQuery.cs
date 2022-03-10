using MediatR;

namespace CODWER.RERU.Evaluation.Application.Tests.Internal.GetTestIdForFastStart
{
    public class GetTestIdForFastStartQuery : IRequest<int>
    {
        public string CoreUserProfileId { get; set; }
    }
}
