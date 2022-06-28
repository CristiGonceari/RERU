using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.Application.Users.CreateUser;
using CODWER.RERU.Core.Application.Users.EditUserFromColaborator;
using CVU.ERP.Common.DataTransferObjects.Files;
using MediatR;
using OfficeOpenXml;
using RERU.Data.Persistence.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CVU.ERP.StorageService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Core.Application.Users.BulkImportUsers
{
    public class BulkImportUsersCommandHandler : IRequestHandler<BulkImportUsersCommand, FileDataDto>
    {
        private readonly IStorageFileService _storageFileService;
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;

        public BulkImportUsersCommandHandler(IStorageFileService storageFileService, IConfiguration configuration, IMediator mediator)
        {
            _storageFileService = storageFileService;
            _configuration = configuration;
            _mediator = mediator;
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

            var tasks = new List<Task>();

            int i = 1;

            while (ExistentRecord(workSheet, i))
            {
                var idnp = workSheet.Cells[i, 4]?.Value?.ToString();

                UserProfile user;

                await using (var db = AppDbContext.NewInstance(_configuration))
                {
                    user = await db.UserProfiles.FirstOrDefaultAsync(x => x.Idnp == idnp);
                }

                tasks.Add(AddEditUser(workSheet, request, i, user));

                i++;

                if (tasks.Count <= 10) continue;
                WaitTasks(Task.WhenAll(tasks));

                tasks.Clear();
            }

            WaitTasks(Task.WhenAll(tasks));
            tasks.Clear();

            var excelFile = GetExcelFile(package);

            await SaveExcelFile(request.ProcessId, excelFile);

            return excelFile;
        }

        private async Task WaitTasks(Task t)
        {
            try
            {
                t.Wait();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception(e.Message);
            }
        }

        private bool ExistentRecord(ExcelWorksheet workSheet, int i)
        {
            return workSheet.Cells[i, 1]?.Value != null && workSheet.Cells[i, 2]?.Value != null;
        }

        private async Task AddEditUser(ExcelWorksheet workSheet, BulkImportUsersCommand request, int i, UserProfile user)
        {
            if (user != null)
            {
                await EditUser(workSheet, user, request, i);
            }
            else
            {
                await CreateUser(workSheet, request, i);
            }
        }

        private async Task SetTotalNumberOfProcesses(int processId, int totalUsers)
        {
            await using (var db = AppDbContext.NewInstance(_configuration))
            {
                var process = await db.Processes.FirstAsync(x => x.Id == processId);
                process.Total = totalUsers;

                await db.SaveChangesAsync();
            }
        }

        private async Task EditUser(ExcelWorksheet workSheet, UserProfile user, BulkImportUsersCommand request, int i)
        {
            try
            {
                var editCommand = GetEditUserCommand(workSheet, user, i);

                await _mediator.Send(editCommand);

                await UpdateProcesses(request.ProcessId);

                workSheet.Cells[i, 11].Value = "Editat";
            }
            catch (Exception e)
            {
                workSheet.Cells[i, 11].Value = $"Error: {e.Message}";
                Console.WriteLine(e);
            }
        }

        private async Task CreateUser(ExcelWorksheet workSheet, BulkImportUsersCommand request, int i)
        {
            try
            {
                var command = GetCreateUserCommand(workSheet, i);

                await _mediator.Send(command);

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
            await using (var db = AppDbContext.NewInstance(_configuration))
            {
                var process = await db.Processes.FirstAsync(x => x.Id == processId);
                process.Done++;

                await db.SaveChangesAsync();
            }
        }

        private async Task SaveExcelFile(int processId, FileDataDto excelFile)
        {
            var fileId = await _storageFileService.AddFile(excelFile.Name, CVU.ERP.StorageService.Entities.FileTypeEnum.procesfile, excelFile.ContentType, excelFile.Content);

            await using (var db = AppDbContext.NewInstance(_configuration))
            {
                var process =await db.Processes.FirstAsync(x => x.Id == processId);

                process.FileId = fileId;
                process.IsDone = true;

                await db.SaveChangesAsync();
            }
        }

        private CreateUserCommand GetCreateUserCommand(ExcelWorksheet workSheet, int i)
        {
            var dateStrings = DateTime.Parse(workSheet.Cells[i, 9]?.Value?.ToString()).ToString("MM.dd.yyyy")?.Split(".");

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
                Birthday = new DateTime(int.Parse(dateStrings[2]), int.Parse(dateStrings[0]), int.Parse(dateStrings[1])),
                PhoneNumber = workSheet.Cells[i, 10]?.Value?.ToString(),
                AccessModeEnum = AccessModeEnum.CurrentDepartment
            };
        }

        private EditUserFromColaboratorCommand GetEditUserCommand(ExcelWorksheet workSheet, UserProfile user, int i)
        {
            var dateStrings = DateTime.Parse(workSheet.Cells[i, 9]?.Value?.ToString()).ToString("MM.dd.yyyy")?.Split(".");

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
