using CODWER.RERU.Personal.DataTransferObjects.EmployeeFunctions;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.EmployeeFunctions.PrintEmployeeFunctions
{
    public class PrintEmployeeFunctionsCommandHandler : IRequestHandler<PrintEmployeeFunctionsCommand, FileDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IExportData<EmployeeFunction, EmployeeFunctionDto> _printer;

        public PrintEmployeeFunctionsCommandHandler(AppDbContext appDbContext, IExportData<EmployeeFunction, EmployeeFunctionDto> printer)
        {
            _appDbContext = appDbContext;
            _printer = printer;
        }

        public async Task<FileDataDto> Handle(PrintEmployeeFunctionsCommand request, CancellationToken cancellationToken)
        {
            var items = _appDbContext.EmployeeFunctions
                .OrderBy(x => x.Name)
                .AsQueryable();

            if (!string.IsNullOrEmpty(request.SearchWord))
            {
                items = items.Where(x => x.Name.ToLower().Contains(request.SearchWord.ToLower()));
            }

            var result = _printer.ExportTableSpecificFormat(new TableData<EmployeeFunction>
            {
                Name = request.TableName,
                Items = items,
                Fields = request.Fields,
                Orientation = request.Orientation,
                ExportFormat = request.TableExportFormat
            });

            return result;
        }
    }
}
