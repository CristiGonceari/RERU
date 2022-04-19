using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.Attributes;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.Locations.PrintLocations
{
    [ModuleOperation(permission: Permissions.PermissionCodes.ACCES_GENERAL_LA_LOCATII)]
    public class PrintLocationCommand : TableParameter, IRequest<FileDataDto>
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
