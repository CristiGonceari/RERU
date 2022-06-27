using CODWER.RERU.Core.Application.Common.Handlers;
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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CVU.ERP.StorageService;
using CVU.ERP.StorageService.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Core.Application.Users.BulkImportUsers
{
    public class BulkImportUsersCommandHandler : IRequestHandler<BulkImportUsersCommand, FileDataDto>
    {
        private readonly IStorageFileService _storageFileService;
        private readonly IMediator Mediator;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;

        public BulkImportUsersCommandHandler(ICommonServiceProvider commonServiceProvider,
            IStorageFileService storageFileService,
             IServiceProvider serviceProvider, IConfiguration configuration)
        {
            //_appDbContext = appDbContext;
            _storageFileService = storageFileService;
            _serviceProvider = serviceProvider;
            _configuration = configuration;

            Mediator = commonServiceProvider.Mediator;



            //_appDbContextFactory = appDbContextFactory;

            //_database = _appDbContextFactory.CreateDbContext();

            //var x = _database.Processes.Count();
        }

        private AppDbContext NewInstance() => new (new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(_configuration.GetConnectionString("RERU"))
            .Options);

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

                using (var db = NewInstance())
                {
                    user = db.UserProfiles.FirstOrDefault(x => x.Idnp == idnp);
                }

                tasks.Add(AddEditUser(workSheet, request, i, user));

                i++;

                if (tasks.Count() <= 2) continue;
                Task.WhenAll(tasks);
                tasks.Clear();
            }

            Task.WhenAll(tasks);
            tasks.Clear();

            var excelFile = GetExcelFile(package);

            await SaveExcelFile(request.ProcessId, excelFile);

            return excelFile;
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
            using (var db = NewInstance())
            {
                var process = db.Processes.First(x => x.Id == processId);
                process.Total = totalUsers;

                await db.SaveChangesAsync();
            }
        }

        private async Task EditUser(ExcelWorksheet workSheet, UserProfile user, BulkImportUsersCommand request, int i)
        {
            try
            {
                var editCommand = GetEditUserCommand(workSheet, user, i);

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

        private async Task CreateUser(ExcelWorksheet workSheet, BulkImportUsersCommand request, int i)
        {
            try
            {
                var command = GetCreateUserCommand(workSheet, i);

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
            using (var db = NewInstance())
            {
                var process = db.Processes.First(x => x.Id == processId);
                process.Done++;

                await db.SaveChangesAsync();
            }
        }

        private async Task SaveExcelFile(int processId, FileDataDto excelFile)
        {
            var fileId = await _storageFileService.AddFile(excelFile.Name, CVU.ERP.StorageService.Entities.FileTypeEnum.procesfile, excelFile.ContentType, excelFile.Content);

            using (var db = NewInstance())
            {
                var process = db.Processes.First(x => x.Id == processId);

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
