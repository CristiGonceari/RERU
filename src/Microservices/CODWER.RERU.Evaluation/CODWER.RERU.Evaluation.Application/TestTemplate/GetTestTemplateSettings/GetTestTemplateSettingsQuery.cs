using CODWER.RERU.Evaluation.DataTransferObjects.TestTypes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.TestTypes.GetTestTypeSettings
{
    public class GetTestTemplateSettingsQuery : IRequest<TestTemplateSettingsDto>
    {
        public int TestTypeId { get; set; }
    }
}
