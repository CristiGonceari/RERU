using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.DataTransferObjects.ModulePermissions;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;
using RERU.Data.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.ModulePermissions.PrintModulePermissions
{
    internal class PrintModulePermissionsCommandHandler : BaseHandler, IRequestHandler<PrintModulePermissionsCommand, FileDataDto>
    {
        private readonly IExportData<ModulePermission, ModulePermissionRowDto> _printer;

        public PrintModulePermissionsCommandHandler(ICommonServiceProvider commonServiceProvider, 
            IExportData<ModulePermission, ModulePermissionRowDto> printer) : base(commonServiceProvider)
        {
            _printer = printer;
        }

        public async Task<FileDataDto> Handle(PrintModulePermissionsCommand request, CancellationToken cancellationToken)
        {
            var moduleRoles = AppDbContext.ModulePermissions.Where(m => m.ModuleId == request.ModuleId);

            moduleRoles = GetAndFilterModulePermissions.Filter(moduleRoles, request.Code, request.Description);

            var result = _printer.ExportTableSpecificFormat(new TableData<ModulePermission>
            {
                Name = request.TableName,
                Items = moduleRoles,
                Fields = request.Fields,
                Orientation = request.Orientation,
                ExportFormat = request.TableExportFormat
            });

            return result;
        }
    }
}
