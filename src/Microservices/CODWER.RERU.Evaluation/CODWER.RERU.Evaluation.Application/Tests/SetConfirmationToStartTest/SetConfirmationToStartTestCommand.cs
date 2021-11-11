using CODWER.RERU.Evaluation.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.Tests.SetConfirmationToStartTest
{
    [ModuleOperation(permission: PermissionCodes.TESTS_GENERAL_ACCESS)]
    public class SetConfirmationToStartTestCommand : IRequest<Unit>
    {
        public int TestId { get; set; }
    }
}
