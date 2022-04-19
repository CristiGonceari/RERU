using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.VerificationTests;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.VerificationTests.GetTestVerificationSummary
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_VERIFICAREA_TESTELOR)]
    public class GetTestVerificationSummaryQuery : IRequest<VerificationTestQuestionDataDto>
    {
        public int TestId { get; set; }
    }
}
