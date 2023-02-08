using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;

namespace CODWER.RERU.Core.Application.ModuleRoles.PrintModuleRoles
{
    public class PrintModuleRolesCommand : TableParameter, IRequest<FileDataDto>
    {
        public int ModuleId { get; set; }
    }
}
