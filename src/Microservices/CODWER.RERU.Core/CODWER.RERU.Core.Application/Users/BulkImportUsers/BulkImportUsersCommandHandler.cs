using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.Application.Users.CreateUser;
using CODWER.RERU.Core.Application.Users.EditUserFromColaborator;
using CVU.ERP.Common.DataTransferObjects.Files;
using MediatR;
using OfficeOpenXml;
using RERU.Data.Persistence.Context;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CVU.ERP.StorageService;
using CVU.ERP.StorageService.Entities;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Core.Application.Users.BulkImportUsers
{
    public class BulkImportUsersCommandHandler : BaseHandler, IRequestHandler<BulkImportUsersCommand, FileDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IStorageFileService _storageFileService;
        

        public BulkImportUsersCommandHandler(ICommonServiceProvider commonServiceProvider,
            AppDbContext appDbContext, IStorageFileService storageFileService) : base(commonServiceProvider)
        {
            _appDbContext = appDbContext;
            _storageFileService = storageFileService;
        }

        public async Task<FileDataDto> Handle(BulkImportUsersCommand request, CancellationToken cancellationToken)
        {
            return await Import(request); 
        }

        private async Task<FileDataDto> Import(BulkImportUsersCommand request)
        {
            var fileStream = new MemoryStream();
            await request.Data.File.CopyToAsync(fileStream);
            using var package = new ExcelPackage(fileStream);
            var workSheet = package.Workbook.Worksheets[0];
            var totalRows = workSheet.Dimension.Rows;

            await SetTotalNumberOfProcesses(request.ProcessId, totalRows);




            for (var i = 1; i <= totalRows; i++)
            {
                var idnp = workSheet.Cells[i, 4]?.Value?.ToString();
                var dateArray = workSheet.Cells[i, 9]?.Value?.ToString()?.Split("/");

                var user = _appDbContext.UserProfiles.FirstOrDefault(x => x.Idnp == idnp);

                if (user != null)
                {
                    await EditUser(workSheet, user, request, i, dateArray);
                }
                else
                {
                    await CreateUser(workSheet, request ,i, dateArray);
                }
            }

            var excelFile = GetExcelFile(package);

            await SaveExcelFile(request.ProcessId, excelFile);

            return excelFile;
        }

        private async Task SetTotalNumberOfProcesses(int processId, int totalUsers)
        {
            var process = _appDbContext.Processes.First(x => x.Id == processId);
            process.Total = totalUsers;

            await _appDbContext.SaveChangesAsync();
        }

        private async Task EditUser(ExcelWorksheet workSheet, UserProfile user, BulkImportUsersCommand request, int i, string[] dateStrings)
        {
            try
            {
                var editCommand = GetEditUserCommand(workSheet, user, i, dateStrings);

                await Mediator.Send(editCommand);

                await UpdateProcesses(request.ProcessId);

                workSheet.Cells[i, 11].Value = "Editat";
            }
            catch (Exception e)
            {
                workSheet.Cells[i, 11].Value = $"Error: {e.Message}";
                Console.WriteLine(e);
            }
        }

        private async Task CreateUser(ExcelWorksheet workSheet, BulkImportUsersCommand request, int i, string[] dateStrings)
        {
            try
            {
                var command = GetCreateUserCommand(workSheet, i, dateStrings);

                await Mediator.Send(command);

                await UpdateProcesses(request.ProcessId);

                workSheet.Cells[i, 11].Value = "Adăugat";
            }
            catch (Exception e)
            {
                workSheet.Cells[i, 11].Value = $"Error: {e.Message}";
                Console.WriteLine(e);
            }
        }

        private async Task UpdateProcesses(int processId)
        {
            var process = _appDbContext.Processes.First(x => x.Id == processId);
            process.Done++;

            await _appDbContext.SaveChangesAsync();
        }

        private async Task SaveExcelFile(int processId, FileDataDto excelFile)
        {
            var fileId = await _storageFileService.AddFile(excelFile.Name, CVU.ERP.StorageService.Entities.FileTypeEnum.procesfile, excelFile.ContentType, excelFile.Content);

            var process = _appDbContext.Processes.First(x => x.Id == processId);

            process.FileId = fileId;
            process.IsDone = true;

            await _appDbContext.SaveChangesAsync();
        }

        private CreateUserCommand GetCreateUserCommand(ExcelWorksheet workSheet, int i, string[] dateStrings)
        {
            return new CreateUserCommand
            {
                LastName = workSheet.Cells[i, 1]?.Value?.ToString(),
                FirstName = workSheet.Cells[i, 2]?.Value?.ToString(),
                FatherName = workSheet.Cells[i, 3]?.Value?.ToString(),
                Idnp = workSheet.Cells[i, 4]?.Value?.ToString(),
                Email = workSheet.Cells[i, 5]?.Value?.ToString(),
                DepartmentColaboratorId = int.Parse(workSheet.Cells[i, 6]?.Value?.ToString() ?? "0"),
                RoleColaboratorId = int.Parse(workSheet.Cells[i, 7]?.Value?.ToString() ?? "0"),
                EmailNotification = bool.Parse(workSheet.Cells[i, 8]?.Value?.ToString() ?? "False"),
                Birthday = new DateTime(int.Parse(dateStrings[2]), int.Parse(dateStrings[1]), int.Parse(dateStrings[0])),
                PhoneNumber = workSheet.Cells[i, 10]?.Value?.ToString(),
                AccessModeEnum = AccessModeEnum.CurrentDepartment
            };
        }

        private EditUserFromColaboratorCommand GetEditUserCommand(ExcelWorksheet workSheet, UserProfile user, int i, string[] dateStrings)
        {
            return new EditUserFromColaboratorCommand()
            {
                Id = user.Id,
                LastName = workSheet.Cells[i, 1]?.Value?.ToString(),
                FirstName = workSheet.Cells[i, 2]?.Value?.ToString(),
                FatherName = workSheet.Cells[i, 3]?.Value?.ToString(),
                Idnp = workSheet.Cells[i, 4]?.Value?.ToString(),
                Email = workSheet.Cells[i, 5]?.Value?.ToString(),
                DepartmentColaboratorId = int.Parse(workSheet.Cells[i, 6]?.Value?.ToString() ?? "0"),
                RoleColaboratorId = int.Parse(workSheet.Cells[i, 7]?.Value?.ToString() ?? "0"),
                EmailNotification = bool.Parse(workSheet.Cells[i, 8]?.Value?.ToString() ?? "True"),
                Birthday = new DateTime(int.Parse(dateStrings[2]), int.Parse(dateStrings[1]), int.Parse(dateStrings[0])),
                PhoneNumber = workSheet.Cells[i, 10]?.Value?.ToString(),
                AccessModeEnum = AccessModeEnum.CurrentDepartment
            };
        }

        private FileDataDto GetExcelFile(ExcelPackage package)
        {
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
