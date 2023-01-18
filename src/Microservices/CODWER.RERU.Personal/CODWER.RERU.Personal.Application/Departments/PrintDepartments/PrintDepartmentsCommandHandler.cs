using CODWER.RERU.Personal.DataTransferObjects.Departments;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.Departments.PrintDepartments
{
    public class PrintDepartmentsCommandHandler : IRequestHandler<PrintDepartmentsCommand, FileDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IExportData<Department, DepartmentDto> _printer;


        public PrintDepartmentsCommandHandler(AppDbContext appDbContext, IExportData<Department, DepartmentDto> printer)
        {
            _appDbContext = appDbContext;
            _printer = printer;
        }

        public async Task<FileDataDto> Handle(PrintDepartmentsCommand request, CancellationToken cancellationToken)
        {
            var items = _appDbContext.Departments
                .AsQueryable();

            if (!string.IsNullOrEmpty(request.Name))
            {
                items = items.Where(x => x.Name.ToLower().Contains(request.Name.ToLower()));
            }

            var result = _printer.ExportTableSpecificFormat(new TableData<Department>
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
