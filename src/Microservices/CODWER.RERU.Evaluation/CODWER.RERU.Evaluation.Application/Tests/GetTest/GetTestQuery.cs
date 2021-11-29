using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.Tests.GetTest
{
    public class GetTestQuery : IRequest<TestDto>
    {
        public int Id { get; set; }
    }
}
