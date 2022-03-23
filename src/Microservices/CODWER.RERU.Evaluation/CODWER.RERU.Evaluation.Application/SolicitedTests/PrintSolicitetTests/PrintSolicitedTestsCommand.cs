using CODWER.RERU.Evaluation.Application.Permissions;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.Attributes;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.SolicitedTests.PrintSolicitetTests
{
    [ModuleOperation(permission: PermissionCodes.SOLICITED_TESTS_GENERAL_ACCESS)]
    public class PrintSolicitedTestsCommand : TableParameter, IRequest<FileDataDto>
    {
    }
}
