using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.Attributes;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.EventEvaluators.PrintEventEvaluators
{
    [ModuleOperation(permission: Permissions.PermissionCodes.ACCES_GENERAL_LA_EVENIMENTE)]
    public class PrintEventEvaluatorsCommand : TableParameter, IRequest<FileDataDto>
    {
        public int EventId { get; set; }
    }
}
