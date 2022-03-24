using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.SolicitedTests;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.SolicitedTests.GetSolicitedTests
{
    [ModuleOperation(permission: PermissionCodes.SOLICITED_TESTS_GENERAL_ACCESS)]
    public class GetSolicitedTestsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<SolicitedTestDto>>
    {
        public string EventName { get; set; }
        public string UserName { get; set; }
        public string TestName { get; set; }
    }
}
