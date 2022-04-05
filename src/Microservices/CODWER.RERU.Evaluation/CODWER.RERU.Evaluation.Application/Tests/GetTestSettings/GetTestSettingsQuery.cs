using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.Tests.GetTestSettings
{
    public class GetTestSettingsQuery : IRequest<TestDto>
    {
        public int Id { get; set; }
       
    }
}
