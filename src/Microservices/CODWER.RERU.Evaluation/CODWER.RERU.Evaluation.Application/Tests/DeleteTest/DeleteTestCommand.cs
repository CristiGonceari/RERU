using CODWER.RERU.Evaluation.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.Tests.DeleteTest
{
    [ModuleOperation(permission: PermissionCodes.TESTS_GENERAL_ACCESS)]
    public class DeleteTestCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
