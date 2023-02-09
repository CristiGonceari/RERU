using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;

namespace CODWER.RERU.Core.Application.ModulePermissions.PrintModulePermissions
{
    public class PrintModulePermissionsCommand : TableParameter, IRequest<FileDataDto>
    {
        public int ModuleId { get; set; }
        public string Code { set; get; }
        public string Description { set; get; }
    }
}
