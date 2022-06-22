using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.BulkProcesses;
using CVU.ERP.Module.Application.Attributes;
using MediatR;
using System.Collections.Generic;


namespace CODWER.RERU.Evaluation.Application.TestImportProcesses.GetProcessHistory
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_TESTE)]

    public class GetBulkProcessHistoryQuery : IRequest<List<HistoryProcessDto>>
    {
    }
}
