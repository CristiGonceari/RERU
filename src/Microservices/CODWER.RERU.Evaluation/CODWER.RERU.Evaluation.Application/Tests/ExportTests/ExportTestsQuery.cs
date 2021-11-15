using CODWER.RERU.Evaluation.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.Tests.ExportTests
{
    [ModuleOperation(permission: PermissionCodes.TESTS_GENERAL_ACCESS)]
    public class ExportTestsQuery : IRequest<byte[]>
    {
    }
}
