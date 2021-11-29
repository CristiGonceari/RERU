using CODWER.RERU.Evaluation.DataTransferObjects.TestTypes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.TestTypes.GetTestTypeSettings
{
    public class GetTestTypeSettingsQuery : IRequest<TestTypeSettingsDto>
    {
        public int TestTypeId { get; set; }
    }
}
