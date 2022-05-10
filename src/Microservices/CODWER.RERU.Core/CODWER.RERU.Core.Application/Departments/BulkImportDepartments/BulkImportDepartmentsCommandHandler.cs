using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CVU.ERP.Common.DataTransferObjects.Files;
using MediatR;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Departments.AddDepartment;
using CODWER.RERU.Core.Application.Departments.UpdateDepartment;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.Departments.BulkImportDepartments
{
    public class BulkImportDepartmentsCommandHandler : BaseHandler, IRequestHandler<BulkImportDepartmentsCommand, FileDataDto>
    {
        private readonly AppDbContext _appDbContext;

        public BulkImportDepartmentsCommandHandler(ICommonServiceProvider commonServiceProvider, AppDbContext appDbContext) : base(commonServiceProvider)
        {
            _appDbContext = appDbContext;
        }

        public async Task<FileDataDto> Handle(BulkImportDepartmentsCommand request, CancellationToken cancellationToken)
        {
            return await Import(request.Data.File);
        }

        public async Task<FileDataDto> Import(IFormFile data)
        {
            var fileStream = new MemoryStream();
            await data.CopyToAsync(fileStream);
            using var package = new ExcelPackage(fileStream);
            var workSheet = package.Workbook.Worksheets[0];
            var totalRows = workSheet.Dimension.Rows;

            var departments = _appDbContext.Departments.AsQueryable();

            for (var i = 1; i <= totalRows; i++)
            {
                var addCommand = new AddDepartmentCommand()
                {
                    ColaboratorId = int.Parse(workSheet.Cells[i, 1]?.Value?.ToString() ?? string.Empty),
                    Name = workSheet.Cells[i, 2]?.Value?.ToString()
                };

                if (departments.Any(x => x.ColaboratorId == addCommand.ColaboratorId))
                {
                    try
                    {
                        var departmentByColaborator = departments.FirstOrDefault(x => x.ColaboratorId == addCommand.ColaboratorId);

                        var editCommand = new UpdateDepartmentCommand()
                        {
                            Id = departmentByColaborator.Id,
                            Name = workSheet.Cells[i, 2]?.Value?.ToString(),
                            ColaboratorId = departmentByColaborator.ColaboratorId
                        };

                        await Mediator.Send(editCommand);
                    }
                    catch (Exception e)
                    {
                        workSheet.Cells[i, 3].Value = e.Message;
                        Console.WriteLine(e);
                    }
                }
                else
                {
                    try
                    {
                        await Mediator.Send(addCommand);
                    }
                    catch (Exception e)
                    {
                        workSheet.Cells[i, 3].Value = e.Message;
                        Console.WriteLine(e);
                    }
                }
            }

            var streamBytesArray = package.GetAsByteArray();

            return new FileDataDto
            {
                Content = streamBytesArray,
                Name = "Department-Import-Result",
                ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            };
        }
    }
}
