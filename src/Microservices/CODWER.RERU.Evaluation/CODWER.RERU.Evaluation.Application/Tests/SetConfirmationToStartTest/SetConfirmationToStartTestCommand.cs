using CODWER.RERU.Evaluation.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.Tests.SetConfirmationToStartTest
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_TESTE)]
    public class SetConfirmationToStartTestCommand : IRequest<Unit>
    {
        public int TestId { get; set; }
    }
}
