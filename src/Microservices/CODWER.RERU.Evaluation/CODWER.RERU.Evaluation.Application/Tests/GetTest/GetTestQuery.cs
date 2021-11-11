using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.Tests.GetTest
{
    [ModuleOperation(permission: PermissionCodes.TESTS_GENERAL_ACCESS)]
    public class GetTestQuery : IRequest<TestDto>
    {
        public int Id { get; set; }
    }
}
