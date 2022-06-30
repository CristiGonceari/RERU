using CODWER.RERU.Evaluation.DataTransferObjects.TestTemplates;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.TestTemplates.GetTestTemplate
{
    public class GetTestTemplateQuery : IRequest<TestTemplateDto>
    {
        public int Id { get; set; }
    }
}
