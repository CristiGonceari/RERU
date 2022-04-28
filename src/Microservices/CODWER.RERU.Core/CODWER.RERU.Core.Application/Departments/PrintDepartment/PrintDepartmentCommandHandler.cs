using System;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Core.DataTransferObjects.Articles;
using CODWER.RERU.Core.DataTransferObjects.Departemnts;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.Departments.PrintDepartment
{
    public class PrintDepartmentCommandHandler : IRequestHandler<PrintDepartmentCommand, FileDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IExportData<Department, DepartmentDto> _printer;

        public PrintDepartmentCommandHandler(AppDbContext appDbContext, IExportData<Department, DepartmentDto> printer)
        {
            _appDbContext = appDbContext;
            _printer = printer;
        }

        public async Task<FileDataDto> Handle(PrintDepartmentCommand request, CancellationToken cancellationToken)
        {
            var deparments = GetAndFilterDepartments.Filter(_appDbContext, request.Name);

            var result = _printer.ExportTableSpecificFormat(new TableData<Department>
            {
                Name = request.TableName,
                Items = deparments,
                Fields = request.Fields,
                Orientation = request.Orientation,
                ExportFormat = request.TableExportFormat
            });

            return result;
        }
    }
}
