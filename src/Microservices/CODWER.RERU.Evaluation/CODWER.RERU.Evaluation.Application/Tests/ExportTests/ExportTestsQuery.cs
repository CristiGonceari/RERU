using CODWER.RERU.Evaluation.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.Tests.ExportTests
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_TESTE)]
    public class ExportTestsQuery : IRequest<byte[]>
    {
    }
}
