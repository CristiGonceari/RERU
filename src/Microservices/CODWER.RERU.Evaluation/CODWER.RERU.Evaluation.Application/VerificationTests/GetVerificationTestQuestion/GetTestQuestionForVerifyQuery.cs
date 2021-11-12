using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.VerificationTests;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.VerificationTests.GetVerificationTestQuestion
{
    [ModuleOperation(permission: PermissionCodes.TEST_VERIFICATION_GENERAL_ACCESS)]
    public class GetTestQuestionForVerifyQuery : IRequest<VerificationTestQuestionUnitDto>
    {
        public VerificationTestQuestionDto Data { get; set; }
    }
}
