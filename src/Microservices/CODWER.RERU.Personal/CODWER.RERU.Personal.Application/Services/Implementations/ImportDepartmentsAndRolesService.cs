using AutoMapper;
using CODWER.RERU.Personal.DataTransferObjects.Departments;
using CODWER.RERU.Personal.DataTransferObjects.Files;
using CODWER.RERU.Personal.DataTransferObjects.OrganizationRoles;
using CVU.ERP.Common.DataTransferObjects.Files;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.Services.Implementations
{
    public class ImportDepartmentsAndRolesService : IImportDepartmentsAndRolesService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private ExcelWorksheet _workSheet;
        private ExcelPackage _package;
        private int _totalRows;
        private List<Role> _roles;
        private List<Department> _departments;


        public ImportDepartmentsAndRolesService(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<FileDataDto> Import(IFormFile data, ImportTypeEnum type)
        {
            await ReadExcelFile(data);

            await GetDepartmentsOrRoles(type);

            for (var i = 1; i <= _totalRows; i++)
            {
                await AddEditData(type, i);
            }

            var streamBytesArray = _package.GetAsByteArray();

            return FileDataDto.GetExcel("Role-Import-Result", streamBytesArray);
        }

        private async Task ReadExcelFile(IFormFile data)
        {
            var fileStream = new MemoryStream();
            await data.CopyToAsync(fileStream);
            _package = new ExcelPackage(fileStream);
            _workSheet = _package.Workbook.Worksheets[0];
            _totalRows = _workSheet.Dimension.Rows;
        }
        
        private async Task GetDepartmentsOrRoles(ImportTypeEnum type)
        {
            if (type == ImportTypeEnum.ImportRoles)
            {
                _roles = _appDbContext.Roles.Select(x => new Role { ColaboratorId = x.ColaboratorId, Name = x.Name }).ToList();
            }
            else
            {
                _departments = _appDbContext.Departments.Select(x => new Department { ColaboratorId = x.ColaboratorId, Name = x.Name }).ToList();

            }
        }

        private async Task AddEditData(ImportTypeEnum type, int i)
        {
            var colaboratorId = int.Parse(_workSheet.Cells[i, 1]?.Value?.ToString() ?? string.Empty);

            if (type == ImportTypeEnum.ImportRoles)
            {

                await AddEditRole(_workSheet, colaboratorId, i);
            }
            else
            {

                await AddEditDepartment(_workSheet, colaboratorId, i);
            }
        }

        private async Task AddEditRole(ExcelWorksheet workSheet, int colaboratorId, int i)
        {
            try
            {
                var roleDto = await GetRoleDto(colaboratorId, workSheet, i);

                var role = _appDbContext.Roles.FirstOrDefault(x => x.ColaboratorId == colaboratorId);

                if (role != null)
                {
                    await EditRole(role, roleDto, workSheet, i);
                }
                else
                {
                    await AddRole(roleDto, workSheet, i);
                }

                await _appDbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                workSheet.Cells[i, 5].Value = $"Error: {e.Message}";
                Console.WriteLine(e);
            }
        }

        private async Task EditRole(Role role, RoleDto roleDto, ExcelWorksheet workSheet, int i)
        {
            if (ValidateNameOnEdit(roleDto))
            {
                workSheet.Cells[i, 5].Value = "Error: Nume dublicat";
            }
            else
            {
                _mapper.Map(roleDto, role);
                workSheet.Cells[i, 5].Value = "Editat";
            }
        }

        private async Task AddRole(RoleDto roleDto, ExcelWorksheet workSheet, int i)
        {
            if (ValidateNameOnCreate(roleDto.Name, ImportTypeEnum.ImportRoles))
            {
                workSheet.Cells[i, 5].Value = "Error: Nume dublicat";
            }
            else
            {
                await _appDbContext.Roles.AddAsync(_mapper.Map<Role>(roleDto));
                workSheet.Cells[i, 5].Value = "Adăugat";
            }
        }

        private async Task AddEditDepartment(ExcelWorksheet workSheet, int colaboratorId, int i)
        {
            try
            {
                var departmentDto = await GetDepartmentDto(colaboratorId, workSheet, i);

                var department = _appDbContext.Departments.FirstOrDefault(x => x.ColaboratorId == colaboratorId);

                if (department != null)
                {
                    await EditDepartment(departmentDto, department, workSheet, i);
                }
                else
                {
                    await AddDepartment(departmentDto, workSheet, i);
                }

                await _appDbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                workSheet.Cells[i, 3].Value = $"Error: {e.Message}";
                Console.WriteLine(e);
            }
        }

        private async Task EditDepartment(DepartmentDto departmentDto, Department department, ExcelWorksheet workSheet, int i)
        {
            if (ValidateNameOnEdit(departmentDto))
            {
                workSheet.Cells[i, 3].Value = "Error: Nume dublicat";
            }
            else
            {
                _mapper.Map(departmentDto, department);
                workSheet.Cells[i, 3].Value = "Editat";
            }
        }

        private async Task AddDepartment(DepartmentDto departmentDto, ExcelWorksheet workSheet, int i)
        {
            if (ValidateNameOnCreate(departmentDto.Name, ImportTypeEnum.ImportDepartments))
            {
                workSheet.Cells[i, 3].Value = "Error: Nume dublicat";
            }
            else
            {
                await _appDbContext.Departments.AddAsync(_mapper.Map<Department>(departmentDto));
                workSheet.Cells[i, 3].Value = "Adaugat";
            }
        }

        private async Task<RoleDto> GetRoleDto(int colaboratorId, ExcelWorksheet workSheet, int i)
        {
            return new RoleDto
            {
                ColaboratorId = colaboratorId,
                Name = workSheet.Cells[i, 2]?.Value?.ToString(),
                ShortCode = workSheet.Cells[i, 3]?.Value?.ToString(),
                Code = workSheet.Cells[i, 4]?.Value?.ToString(),
            };
        }

        private async Task<DepartmentDto> GetDepartmentDto(int colaboratorId, ExcelWorksheet workSheet, int i)
        {
            return new DepartmentDto
            {
                Name = workSheet.Cells[i, 2]?.Value?.ToString(),
                ColaboratorId = colaboratorId
            };
        }

        private bool ValidateNameOnEdit(RoleDto dto) => _roles.Any(r => r.ColaboratorId != dto.ColaboratorId &&
                                                                        (string.Equals(r.Name, dto.Name, StringComparison.CurrentCultureIgnoreCase) ||
                                                                         string.Equals(dto.Name, r.Name, StringComparison.CurrentCultureIgnoreCase)));

        private bool ValidateNameOnEdit(DepartmentDto dto) => _departments.Any(d => d.ColaboratorId != dto.ColaboratorId &&
            (string.Equals(d.Name, dto.Name, StringComparison.CurrentCultureIgnoreCase) ||
             string.Equals(dto.Name, d.Name, StringComparison.CurrentCultureIgnoreCase)));

        private bool ValidateNameOnCreate(string name, ImportTypeEnum type)
        {
            if (type == ImportTypeEnum.ImportRoles)
            {
                return _roles.Any(r =>
                    string.Equals(r.Name, name, StringComparison.CurrentCultureIgnoreCase) ||
                    string.Equals(name, r.Name, StringComparison.CurrentCultureIgnoreCase));
            }

            return _departments.Any(d =>
                string.Equals(d.Name, name, StringComparison.CurrentCultureIgnoreCase) ||
                string.Equals(name, d.Name, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
