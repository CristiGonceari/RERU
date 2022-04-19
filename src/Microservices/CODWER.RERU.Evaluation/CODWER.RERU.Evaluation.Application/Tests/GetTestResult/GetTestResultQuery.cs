using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.Tests.GetTestResult
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_TESTE)]
    public class GetTestResultQuery : IRequest<TestResultDto>
    {
        public int TestId { get; set; }
    }
}
