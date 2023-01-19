using AutoMapper;
using CODWER.RERU.Personal.DataTransferObjects.EmployeeFunctions;
using CVU.ERP.Common.DataTransferObjects.Files;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.Services.Implementations
{
    public class ImportEmployeeFunctionsService : IImportEmployeeFunctionsService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private ExcelWorksheet _workSheet;
        private ExcelPackage _package;
        private int _totalRows;
        private List<EmployeeFunction> _functions;

        public ImportEmployeeFunctionsService(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<FileDataDto> Import(IFormFile data)
        {
            await ReadExcelFile(data);

            _functions = _appDbContext.EmployeeFunctions.Select(x => new EmployeeFunction { ColaboratorId = x.ColaboratorId, Name = x.Name }).ToList();

            for (var i = 1; i <= _totalRows; i++)
            {
                if (!string.IsNullOrEmpty(_workSheet.Cells[i, 1]?.Value?.ToString() ?? string.Empty) &&
                    !string.IsNullOrEmpty(_workSheet.Cells[i, 2]?.Value?.ToString() ?? string.Empty) &&
                    !string.IsNullOrEmpty(_workSheet.Cells[i, 3]?.Value?.ToString() ?? string.Empty))
                {
                    await AddEditData(i);
                }
                else
                {
                    _workSheet.Cells[i, 5].Value = $"Error: Fișierul trebuie să conțină în mod necesar colaboratorId și nume si tipul funcției";
                }
            }

            var streamBytesArray = _package.GetAsByteArray();

            return FileDataDto.GetExcel("Employee-functions-Import-Result", streamBytesArray);
        }

        private async Task ReadExcelFile(IFormFile data)
        {
            var fileStream = new MemoryStream();
            await data.CopyToAsync(fileStream);
            _package = new ExcelPackage(fileStream);
            _workSheet = _package.Workbook.Worksheets[0];
            _totalRows = _workSheet.Dimension.Rows;
        }

        private async Task AddEditData(int i)
        {
            var colaboratorId = int.Parse(_workSheet.Cells[i, 1]?.Value?.ToString() ?? string.Empty);

            await AddEditEmployeeFunction(_workSheet, colaboratorId, i);
        }

        private async Task AddEditEmployeeFunction(ExcelWorksheet workSheet, int colaboratorId, int i)
        {
            try
            {
                var employeeFunctionDto = await GetEmployeeFunction(colaboratorId, workSheet, i);

                var employeeFunction = _appDbContext.EmployeeFunctions.FirstOrDefault(x => x.ColaboratorId == colaboratorId);

                if (employeeFunction is not null)
                {
                    await EditEmployeeFunction(employeeFunction, employeeFunctionDto, workSheet, i);
                }
                else
                {
                    await AddEmployeeFunction(employeeFunctionDto, workSheet, i);
                }

                await _appDbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                workSheet.Cells[i, 5].Value = $"Error: {e.Message}";
                Console.WriteLine(e);
            }
        }

        private async Task EditEmployeeFunction(EmployeeFunction employeeFunction, EmployeeFunctionDto employeeFunctionDto, ExcelWorksheet workSheet, int i)
        {
            if (ValidateNameOnEdit(employeeFunctionDto))
            {
                workSheet.Cells[i, 5].Value = "Error: Nume duplicat";
            }
            else
            {
                _mapper.Map(employeeFunctionDto, employeeFunction);
                workSheet.Cells[i, 5].Value = "Editat";
            }
        }

        private async Task AddEmployeeFunction(EmployeeFunctionDto employeeFunctionDto, ExcelWorksheet workSheet, int i)
        {
            if (ValidateNameOnCreate(employeeFunctionDto.Name))
            {
                workSheet.Cells[i, 5].Value = "Error: Nume duplicat";
            }
            else
            {
                await _appDbContext.EmployeeFunctions.AddAsync(_mapper.Map<EmployeeFunction>(employeeFunctionDto));
                workSheet.Cells[i, 5].Value = "Adăugat";
            }
        }

        private async Task<EmployeeFunctionDto> GetEmployeeFunction(int colaboratorId, ExcelWorksheet workSheet, int i) =>
            new()
            {
                ColaboratorId = colaboratorId,
                Name = workSheet.Cells[i, 2]?.Value?.ToString(),
                Type = workSheet.Cells[i, 3]?.Value?.ToString(),
            };

        private bool ValidateNameOnCreate(string name) =>
            _functions.Any(r =>
                string.Equals(r.Name, name, StringComparison.CurrentCultureIgnoreCase) ||
                string.Equals(name, r.Name, StringComparison.CurrentCultureIgnoreCase));

        private bool ValidateNameOnEdit(EmployeeFunctionDto dto) => _functions.Any(d => d.ColaboratorId != dto.ColaboratorId &&
            (string.Equals(d.Name, dto.Name, StringComparison.CurrentCultureIgnoreCase) ||
             string.Equals(dto.Name, d.Name, StringComparison.CurrentCultureIgnoreCase)));
    }
}
      
