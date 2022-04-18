using CODWER.RERU.Evaluation.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.Application.Tests.EditTestStatus
{
    //[ModuleOperation(permission: PermissionCodes.TESTS_GENERAL_ACCESS)]
    public class EditTestStatusCommand : IRequest<Unit>
    {
        public int TestId { get; set; }
        public TestStatusEnum Status { get; set; }
    }
}
