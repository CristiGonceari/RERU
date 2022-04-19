using CODWER.RERU.Evaluation.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.VerificationTests.FinalizeTestVerification
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_VERIFICAREA_TESTELOR)]
    public class FinalizeTestVerificationCommand : IRequest<Unit>
    {
        public int TestId { get; set; }
    }
}
