using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.SolicitedTests;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.SolicitedTests.GetSolicitedTest
{
    [ModuleOperation(permission: PermissionCodes.SOLICITED_TESTS_GENERAL_ACCESS)]
    public class GetSolicitedTestQuery : IRequest<SolicitedTestDto>
    {
        public int Id { get; set; }
    }
}
