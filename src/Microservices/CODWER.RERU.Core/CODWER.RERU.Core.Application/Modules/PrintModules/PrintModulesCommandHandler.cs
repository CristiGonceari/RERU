using CODWER.RERU.Core.DataTransferObjects.Modules;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.Modules.PrintModules
{
    public class PrintModulesCommandHandler : IRequestHandler<PrintModulesCommand, FileDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IExportData<global::RERU.Data.Entities.Module, ModuleDto> _printer;

        public PrintModulesCommandHandler(AppDbContext appDbContext, IExportData<global::RERU.Data.Entities.Module, ModuleDto> printer)
        {
            _appDbContext = appDbContext;
            _printer = printer;
        }

        public async Task<FileDataDto> Handle(PrintModulesCommand request, CancellationToken cancellationToken)
        {
            var modules = GetAndPrintModules.Filter(_appDbContext, request.Keyword);

            var result = _printer.ExportTableSpecificFormat(new TableData<global::RERU.Data.Entities.Module>
            {
                Name = request.TableName,
                Items = modules,
                Fields = request.Fields,
                Orientation = request.Orientation,
                ExportFormat = request.TableExportFormat
            });

            return result;
        }
    }
}
