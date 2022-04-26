using CODWER.RERU.Core.Application.Permissions;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.Attributes;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;

namespace CODWER.RERU.Core.Application.Modules.PrintModules
{
    [ModuleOperation(permission: PermissionCodes.VIZUALIZAREA_MODULELOR)]

    public class PrintModulesCommand : TableParameter, IRequest<FileDataDto>
    {
        public string Keyword { get; set; }
    }
}
