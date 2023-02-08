using CODWER.RERU.Evaluation.Application.Permissions;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.Attributes;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.EventResponsiblePersons.PrintEventResponsiblePersons
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_EVENIMENTE)]

    public class PrintEventResponsiblePersonsCommand : TableParameter, IRequest<FileDataDto>
    {
        public int EventId { get; set; }
    }
}
