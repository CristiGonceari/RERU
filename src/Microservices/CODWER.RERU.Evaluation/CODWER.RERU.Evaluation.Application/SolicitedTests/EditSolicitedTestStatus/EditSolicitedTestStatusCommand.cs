using CODWER.RERU.Evaluation.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.Application.SolicitedTests.EditSolicitedTestStatus
{
    [ModuleOperation(permission: PermissionCodes.SOLICITED_TESTS_GENERAL_ACCESS)]
    public class EditSolicitedTestStatusCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public SolicitedTestStatusEnum Status { get; set; }
    }
}
