using MediatR;

namespace CODWER.RERU.Evaluation.Application.Tests.GenerateTestResultFile
{
    public class GenerateTestResultFileCommand : IRequest<int>
    {
        public int TestId { get; set; }
    }
}
