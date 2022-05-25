using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.Application.Users.CreateUser;
using CVU.ERP.Common.DataTransferObjects.Files;
using MediatR;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Core.DataTransferObjects.UserProfiles;
using CVU.ERP.Common.DataTransferObjects.Users;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.Users.BulkImportUsers
{
    public class BulkImportUsersCommandHandler : BaseHandler, IRequestHandler<BulkImportUsersCommand, FileDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public BulkImportUsersCommandHandler(ICommonServiceProvider commonServiceProvider, AppDbContext appDbContext, IMapper mapper) : base(commonServiceProvider)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<FileDataDto> Handle(BulkImportUsersCommand request, CancellationToken cancellationToken)
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

            for (var i = 1; i <= totalRows; i++)
            {
                var newUser = new CreateUserDto()
                {
                    LastName = workSheet.Cells[i, 1]?.Value?.ToString(),
                    FirstName = workSheet.Cells[i, 2]?.Value?.ToString(),
                    FatherName = workSheet.Cells[i, 3]?.Value?.ToString(),
                    Idnp = workSheet.Cells[i, 4]?.Value?.ToString(),
                    Email = workSheet.Cells[i, 5]?.Value?.ToString(),
                    DepartmentColaboratorId = int.Parse(workSheet.Cells[i, 6]?.Value?.ToString() ?? "0"),
                    RoleColaboratorId = int.Parse(workSheet.Cells[i, 7]?.Value?.ToString() ?? "0"),
                    EmailNotification = bool.Parse(workSheet.Cells[i, 8]?.Value?.ToString() ?? "True")
                };

                var user = _appDbContext.UserProfiles.FirstOrDefault(x => x.Idnp == newUser.Idnp);

                if (user != null)
                {
                    try
                    {
                        _mapper.Map(newUser, user);
                        await _appDbContext.SaveChangesAsync();

                        workSheet.Cells[i, 9].Value = "Editat";
                    }
                    catch (Exception e)
                    {
                        workSheet.Cells[i, 9].Value = $"Error: {e.Message}";
                        Console.WriteLine(e);
                    }
                }
                else
                {
                    try
                    {
                        await _appDbContext.UserProfiles.AddAsync(_mapper.Map<UserProfile>(newUser));
                        await _appDbContext.SaveChangesAsync();

                        workSheet.Cells[i, 9].Value = "Adăugat";
                    }
                    catch (Exception e)
                    {
                        workSheet.Cells[i, 9].Value = $"Error: {e.Message}";
                        Console.WriteLine(e);
                    }
                }
            }

            var streamBytesArray = package.GetAsByteArray();

            return new FileDataDto
            {
                Content = streamBytesArray,
                Name = "User-Import-Result",
                ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            };
        }
    }
}
