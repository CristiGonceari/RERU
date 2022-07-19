using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.DataTransferObjects.Roles;
using CVU.ERP.Common.DataTransferObjects.Files;
using MediatR;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using RERU.Data.Entities;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.Roles.BulkImportRoles
{
    public class BulkImportRolesCommandHandler : BaseHandler, IRequestHandler<BulkImportRolesCommand, FileDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        private List<Role> roles;

        public BulkImportRolesCommandHandler(ICommonServiceProvider commonServiceProvider, AppDbContext appDbContext, IMapper mapper) : base(commonServiceProvider)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<FileDataDto> Handle(BulkImportRolesCommand request, CancellationToken cancellationToken)
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

            roles = _appDbContext.Roles.ToList();

            for (var i = 1; i <= totalRows; i++)
            {
                var newRole = new RoleDto()
                {
                    ColaboratorId = int.Parse(workSheet.Cells[i, 1]?.Value?.ToString() ?? string.Empty),
                    Name = workSheet.Cells[i, 2]?.Value?.ToString()
                };

                var role = _appDbContext.Roles.FirstOrDefault(x => x.ColaboratorId == newRole.ColaboratorId);

                if (role != null)
                {
                    try
                    {
                        if (CheckUpdateRole(newRole))
                        {
                            workSheet.Cells[i, 3].Value = "Error: Nume dublicat";
                        }
                        else
                        {
                            _mapper.Map(newRole, role);
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
                        if (CheckCreateRole(newRole.Name))
                        {
                            workSheet.Cells[i, 3].Value = "Error: Nume dublicat";
                        }
                        else
                        {
                            await _appDbContext.Roles.AddAsync(_mapper.Map<Role>(newRole));
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

            return FileDataDto.GetExcel("Role-Import-Result", streamBytesArray);
        }

        private bool CheckCreateRole(string name)
        {
            var result = roles.Any(x => x.Name.ToLower().Contains(name.ToLower()) || name.ToLower().Contains(x.Name.ToLower()));

            return result;
        }

        private bool CheckUpdateRole(RoleDto command)
        {
            var result = roles.Any(x => x.Id != command.Id && (x.Name.ToLower().Contains(command.Name.ToLower()) || command.Name.ToLower().Contains(x.Name.ToLower())));

            return result;
        }
    }
}
