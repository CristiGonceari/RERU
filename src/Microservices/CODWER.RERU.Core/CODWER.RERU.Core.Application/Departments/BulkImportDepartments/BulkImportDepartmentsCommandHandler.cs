using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CVU.ERP.Common.DataTransferObjects.Files;
using MediatR;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Core.DataTransferObjects.Departemnts;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.Departments.BulkImportDepartments
{
    public class BulkImportDepartmentsCommandHandler : BaseHandler, IRequestHandler<BulkImportDepartmentsCommand, FileDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        private List<Department> departments;

        public BulkImportDepartmentsCommandHandler(ICommonServiceProvider commonServiceProvider, AppDbContext appDbContext, IMapper mapper) : base(commonServiceProvider)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;

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

            departments = _appDbContext.Departments.ToList();

            for (var i = 1; i <= totalRows; i++)
            {
                var newDepartment = new DepartmentDto()
                {
                    Name = workSheet.Cells[i, 2]?.Value?.ToString(),
                    ColaboratorId = int.Parse(workSheet.Cells[i, 1]?.Value?.ToString() ?? string.Empty)
                };

                var department = _appDbContext.Departments.FirstOrDefault(x => x.ColaboratorId == newDepartment.ColaboratorId);

                if (department != null)
                {
                    try
                    {
                        if (CheckUpdateDepartment(newDepartment))
                        {
                            workSheet.Cells[i, 3].Value = "Error: Nume dublicat";
                        }
                        else
                        {
                            _mapper.Map(newDepartment, department);
                            await _appDbContext.SaveChangesAsync();

                            workSheet.Cells[i, 3].Value = "Editat";
                        }
                    }
                    catch (Exception e)
                    {
                        workSheet.Cells[i, 3].Value = $"Error: {e.Message}";
                        Console.WriteLine(e);
                    }
                }
                else
                {
                    try
                    {
                        if (CheckCreateDepartment(newDepartment.Name))
                        {
                            workSheet.Cells[i, 3].Value = "Error: Nume dublicat";
                        }
                        else
                        {
                            await _appDbContext.Departments.AddAsync(_mapper.Map<Department>(newDepartment));
                            await _appDbContext.SaveChangesAsync();

                            workSheet.Cells[i, 3].Value = "Adăugat";
                        }
                    }
                    catch (Exception e)
                    {
                        workSheet.Cells[i, 3].Value = $"Error: {e.Message}";
                        Console.WriteLine(e);
                    }
                }
            }

            var streamBytesArray = package.GetAsByteArray();

            return FileDataDto.GetPdf("Department-Import-Result", streamBytesArray);
        }

        private bool CheckCreateDepartment(string name)
        {
            var result = departments.Any(x => x.Name.ToLower().Contains(name.ToLower()) || name.ToLower().Contains(x.Name.ToLower()));

            return result;
        }

        private bool CheckUpdateDepartment(DepartmentDto command)
        {
            var result = departments.Any(x => x.Id != command.Id && (x.Name.ToLower().Contains(command.Name.ToLower()) || command.Name.ToLower().Contains(x.Name.ToLower())));

            return result;
        }
    }
}
