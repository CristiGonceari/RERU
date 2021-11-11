using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.Tests.EditTestStatus
{
    [ModuleOperation(permission: PermissionCodes.TESTS_GENERAL_ACCESS)]
    public class EditTestStatusCommand : IRequest<Unit>
    {
        public int TestId { get; set; }
        public TestStatusEnum Status { get; set; }
    }
}
