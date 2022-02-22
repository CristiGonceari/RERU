using CODWER.RERU.Evaluation.DataTransferObjects.TestTemplates;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.TestTemplates.GetTestTemplateSettings
{
    public class GetTestTemplateSettingsQuery : IRequest<TestTemplateSettingsDto>
    {
        public int TestTemplateId { get; set; }
    }
}
