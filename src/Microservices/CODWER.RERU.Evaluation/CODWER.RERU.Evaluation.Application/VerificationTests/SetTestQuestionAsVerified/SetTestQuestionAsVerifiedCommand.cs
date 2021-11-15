using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.VerificationTests;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.VerificationTests.SetTestQuestionAsVerified
{
    [ModuleOperation(permission: PermissionCodes.TEST_VERIFICATION_GENERAL_ACCESS)]
    public class SetTestQuestionAsVerifiedCommand : IRequest<Unit>
    {
        public VerificationTestQuestionDto Data { get; set; }
    }
}
