using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.DataTransferObjects.ModuleRoles;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;
using RERU.Data.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.ModuleRoles.PrintModuleRoles
{
    public class PrintModuleRolesCommandHandler : BaseHandler, IRequestHandler<PrintModuleRolesCommand, FileDataDto>
    {
        private readonly IExportData<ModuleRole, ModuleRoleRowDto> _printer;

        public PrintModuleRolesCommandHandler(ICommonServiceProvider commonServiceProvider, 
            IExportData<ModuleRole, ModuleRoleRowDto> printer) : base(commonServiceProvider)
        {
            _printer = printer;
        }

        public async Task<FileDataDto> Handle(PrintModuleRolesCommand request, CancellationToken cancellationToken)
        {
            var moduleRoles = AppDbContext.ModuleRoles
                .Where(m => m.ModuleId == request.ModuleId);

            var result = _printer.ExportTableSpecificFormat(new TableData<ModuleRole>
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
