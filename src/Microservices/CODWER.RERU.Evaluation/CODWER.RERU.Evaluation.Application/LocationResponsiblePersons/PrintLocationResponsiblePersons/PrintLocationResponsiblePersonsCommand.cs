using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.Attributes;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.LocationResponsiblePersons.PrintLocationResponsiblePersons
{
    [ModuleOperation(permission: Permissions.PermissionCodes.ACCES_GENERAL_LA_LOCATII)]
    public class PrintLocationResponsiblePersonsCommand : TableParameter, IRequest<FileDataDto>
    {
        public int LocationId { get; set; }
    }
}
