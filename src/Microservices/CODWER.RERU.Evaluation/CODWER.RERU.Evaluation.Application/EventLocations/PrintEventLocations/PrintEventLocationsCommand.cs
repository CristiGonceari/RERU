using CODWER.RERU.Evaluation.Application.Permissions;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.Attributes;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.EventLocations.PrintEventLocations
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_EVENIMENTE)]
    public class PrintEventLocationsCommand : TableParameter, IRequest<FileDataDto>
    {
        public int EventId { get; set; }
    }
}
