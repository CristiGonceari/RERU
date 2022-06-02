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

        public async Task<FileDataDto> Import(BulkImportUsersCommand request)
        {
            var fileStream = new MemoryStream();
            await request.Data.File.CopyToAsync(fileStream);
            using var package = new ExcelPackage(fileStream);
            var workSheet = package.Workbook.Worksheets[0];
            var totalRows = workSheet.Dimension.Rows;

            await SetTotalNumberOfProcesses(request.ProcessId, totalRows);

            for (var i = 1; i <= totalRows; i++)
            {
                var command = new CreateUserCommand
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

                var user = _appDbContext.UserProfiles.FirstOrDefault(x => x.Idnp == command.Idnp);

                if (user != null)
                {
                    try
                    {
                        var editCommand = new EditUserFromColaboratorCommand()
                        {
                            Id = user.Id,
                            LastName = workSheet.Cells[i, 1]?.Value?.ToString(),
                            FirstName = workSheet.Cells[i, 2]?.Value?.ToString(),
                            FatherName = workSheet.Cells[i, 3]?.Value?.ToString(),
                            Idnp = workSheet.Cells[i, 4]?.Value?.ToString(),
                            Email = workSheet.Cells[i, 5]?.Value?.ToString(),
                            DepartmentColaboratorId = int.Parse(workSheet.Cells[i, 6]?.Value?.ToString() ?? "0"),
                            RoleColaboratorId = int.Parse(workSheet.Cells[i, 7]?.Value?.ToString() ?? "0"),
                            EmailNotification = bool.Parse(workSheet.Cells[i, 8]?.Value?.ToString() ?? "True")
                        };

                        await Mediator.Send(editCommand);

                        await UpdateProcesses(request.ProcessId);

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
                        await Mediator.Send(command);

                        await UpdateProcesses(request.ProcessId);

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

            var excelFile = new FileDataDto
            {
                Content = streamBytesArray,
                Name = "User-Import-Result",
                ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            };

            await SaveExcelFile(request.ProcessId, excelFile);

            return excelFile;
        }

        private async Task SetTotalNumberOfProcesses(int processId, int totalUsers)
        {
            var process = _appDbContext.Processes.First(x => x.Id == processId);
            process.Total = totalUsers;

            await _appDbContext.SaveChangesAsync();
        }

        private async Task UpdateProcesses(int processId)
        {
            var process = _appDbContext.Processes.First(x => x.Id == processId);
            process.Done++;

            await _appDbContext.SaveChangesAsync();
        }

        private async Task SaveExcelFile(int processId, FileDataDto excelFile)
        {
            var fileId = await _storageFileService.AddFile(excelFile.Name, FileTypeEnum.procesfile, excelFile.ContentType, excelFile.Content);

            var process = _appDbContext.Processes.First(x => x.Id == processId);

            process.FileId = fileId;
            process.IsDone = true;

            await _appDbContext.SaveChangesAsync();
        }
    }
}
