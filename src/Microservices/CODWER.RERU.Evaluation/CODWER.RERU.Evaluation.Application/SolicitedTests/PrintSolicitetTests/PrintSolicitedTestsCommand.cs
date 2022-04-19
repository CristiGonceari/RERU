using CODWER.RERU.Evaluation.Application.Permissions;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.Attributes;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.SolicitedTests.PrintSolicitetTests
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_TESTE_SOLICITATE)]
    public class PrintSolicitedTestsCommand : TableParameter, IRequest<FileDataDto>
    {
        public string EventName { get; set; }
        public string UserName { get; set; }
        public string TestName { get; set; }
    }
}
