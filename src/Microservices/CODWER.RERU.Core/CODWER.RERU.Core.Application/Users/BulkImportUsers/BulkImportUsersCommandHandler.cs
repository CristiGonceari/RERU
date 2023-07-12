using CODWER.RERU.Core.Application.Users.CreateUser;
using CODWER.RERU.Core.Application.Users.EditUserFromColaborator;
using CODWER.RERU.Core.DataTransferObjects.UserProfiles.BulkImportEnums;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.StorageService;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.Users.BulkImportUsers
{
    public class BulkImportUsersCommandHandler : IRequestHandler<BulkImportUsersCommand, FileDataDto>
    {
        private readonly IStorageFileService _storageFileService;
        private readonly IMediator _mediator;
        private readonly AppDbContext _appDbContext;
        private readonly Color _color = Color.FromArgb(255, 0, 0);
        private readonly Color _colorReset = Color.FromArgb(255, 255, 255);
   
        public BulkImportUsersCommandHandler(AppDbContext appDbContext, 
            IStorageFileService storageFileService, 
            IMediator mediator)
        {
            _appDbContext = appDbContext;
            _storageFileService = storageFileService;
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

            var isValid = await ValidateExcel(workSheet);

            if (!isValid) return await ReturnInvalidExcel(package, request);

            await SetTotalNumberOfProcesses(request.ProcessId, totalRows);

            var tasks = new List<Task>();

            int i = 2;

            while (ExistentRecord(workSheet, i))
            {
                var idnp = workSheet.Cells[i, 4]?.Value?.ToString();

                UserProfile user;

                await using (var db = _appDbContext.NewInstance())
                {
                    user = await db.UserProfiles.FirstOrDefaultAsync(x => x.Idnp == idnp && x.IsDeleted == false);
                }

                var newTask = AddEditUser(workSheet, request, i, user);

                tasks.Add(newTask);

                i++;
            }

            WaitTasks(Task.WhenAll(tasks));
            tasks.Clear();

            var excelFile = GetExcelFile(package, true);

            await SaveExcelFile(request.ProcessId, excelFile);

            return excelFile;
        }

        private async Task<FileDataDto> ReturnInvalidExcel(ExcelPackage package, BulkImportUsersCommand request)
        {
            var excelFile = GetExcelFile(package, false);

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
            await using var db = _appDbContext.NewInstance();
            var process = await db.Processes.FirstAsync(x => x.Id == processId);
            process.Total = totalUsers;

            await db.SaveChangesAsync();
        }

        private async Task EditUser(ExcelWorksheet workSheet, UserProfile user, BulkImportUsersCommand request, int i)
        {
            try
            {
                var editCommand = GetEditUserCommand(workSheet, user, i);

                await _mediator.Send(editCommand);

                await UpdateProcesses(request.ProcessId);

                workSheet.Cells[i, 12].Value = "Editat";
            }
            catch (Exception e)
            {
                workSheet.Cells[i, 12].Value = $"Error: {e.Message}";
            }
        }

        private async Task CreateUser(ExcelWorksheet workSheet, BulkImportUsersCommand request, int i)
        {
            try
            {
                var command = GetCreateUserCommand(workSheet, i);

                await _mediator.Send(command);

                await UpdateProcesses(request.ProcessId);

                workSheet.Cells[i, 12].Value = "Adăugat";
            }
            catch (Exception e)
            {
                workSheet.Cells[i, 12].Value = $"Error: {e.Message}";
            }
        }

        private async Task UpdateProcesses(int processId)
        {
            await using (var db = _appDbContext.NewInstance())
            {
                var process = await db.Processes.FirstAsync(x => x.Id == processId);
                process.Done++;

                await db.SaveChangesAsync();
            }
        }

        private async Task SaveExcelFile(int processId, FileDataDto excelFile)
        {
            var fileId = await _storageFileService.AddFile(excelFile.Name, CVU.ERP.StorageService.Entities.FileTypeEnum.procesfile, excelFile.ContentType, excelFile.Content);

            await using (var db = _appDbContext.NewInstance())
            {
                var process = await db.Processes.FirstAsync(x => x.Id == processId);

                process.FileId = fileId;
                process.IsDone = true;

                await db.SaveChangesAsync();
            }
        }

        private CreateUserCommand GetCreateUserCommand(ExcelWorksheet workSheet, int i)
        {
            String[] delimiters = { ".", "/" };
            var dateStrings = workSheet.Cells[i, 9]?.Value?.ToString().Substring(0, 10).Split(delimiters, StringSplitOptions.None);

            return new CreateUserCommand
            {
                LastName = workSheet.Cells[i, 1]?.Value?.ToString(),
                FirstName = workSheet.Cells[i, 2]?.Value?.ToString(),
                FatherName = workSheet.Cells[i, 3]?.Value?.ToString(),
                Idnp = workSheet.Cells[i, 4]?.Value?.ToString(),
                Email = workSheet.Cells[i, 5]?.Value?.ToString(),
                DepartmentColaboratorId = int.Parse(workSheet.Cells[i, 6]?.Value?.ToString() ?? "0"),
                RoleColaboratorId = int.Parse(workSheet.Cells[i, 7]?.Value?.ToString() ?? "0"),
                FunctionColaboratorId = int.Parse(workSheet.Cells[i, 8]?.Value?.ToString() ?? "0"),
                //EmailNotification = bool.Parse(workSheet.Cells[i, 9]?.Value?.ToString() ?? "False"),
                BirthDate = new DateTime(int.Parse(dateStrings[2]), int.Parse(dateStrings[1]), int.Parse(dateStrings[0])),
                PhoneNumber = workSheet.Cells[i, 10]?.Value?.ToString(),
                AccessModeEnum = AccessModeEnum.CurrentDepartment
            };
        }

        private EditUserFromColaboratorCommand GetEditUserCommand(ExcelWorksheet workSheet, UserProfile user, int i)
        {
            String[] delimiters = { ".", "/" };
            var dateStrings = workSheet.Cells[i, 9]?.Value?.ToString().Substring(0, 10).Split(delimiters, StringSplitOptions.None);

            return new EditUserFromColaboratorCommand()
            {
                Id = user.Id,
                LastName = workSheet.Cells[i, 1]?.Value?.ToString(),
                FirstName = workSheet.Cells[i, 2]?.Value?.ToString(),
                FatherName = workSheet.Cells[i, 3]?.Value?.ToString() ?? user.FatherName,
                Idnp = workSheet.Cells[i, 4]?.Value?.ToString(),
                Email = workSheet.Cells[i, 5]?.Value?.ToString(),
                DepartmentColaboratorId = int.Parse(workSheet.Cells[i, 6]?.Value?.ToString() ?? "0"),
                RoleColaboratorId = int.Parse(workSheet.Cells[i, 7]?.Value?.ToString() ?? "0"),
                FunctionColaboratorId = int.Parse(workSheet.Cells[i, 8]?.Value?.ToString() ?? "0"),
                //EmailNotification = bool.Parse(workSheet.Cells[i, 9]?.Value?.ToString() ?? "True"),
                BirthDate = new DateTime(int.Parse(dateStrings[2]), int.Parse(dateStrings[1]), int.Parse(dateStrings[0])),
                PhoneNumber = workSheet.Cells[i, 10]?.Value?.ToString(),
                AccessModeEnum = AccessModeEnum.CurrentDepartment
            };
        }

        private FileDataDto GetExcelFile(ExcelPackage package, bool isValid)
        {
            var streamBytesArray = package.GetAsByteArray();

            return isValid ? FileDataDto.GetExcel("User-Import-Result", streamBytesArray) : FileDataDto.GetExcel("User-Import-Invalid", streamBytesArray);
        }

        private async Task<bool> ValidateExcel(ExcelWorksheet workSheet)
        {
            bool isValid = true;
            var requiredColumns = new string[] { "Nume", "Prenume", "Patronimic", "Idnp", "Email", "Id Departament", "Id Rol", "Id Funcție", "Data nașterii", "Nr. telefon" };
            var headers = workSheet.Cells[1, 1, 1, workSheet.Dimension.Columns].Select(c => c.Value?.ToString().Trim()).Take(10).ToArray();

            if (!headers.SequenceEqual(requiredColumns, StringComparer.OrdinalIgnoreCase))
            {
                isValid = false;
                for (int i = 0; i < requiredColumns.Length; i++)
                {
					if (!headers.Any(header => string.Equals(header, requiredColumns[i], StringComparison.OrdinalIgnoreCase)))
					{
                        workSheet.Cells[1, 12].Value = "Respectați ordinea coloanelor Nume, Prenume, Patronimic, Idnp, Email, Id Departament, Id Rol, Id Funcție, Data nașterii, Nr. telefon";
                        workSheet.Cells[1, i + 1].Style.Fill.SetBackground(_color);
                    }
                }
            }

            return isValid && await ValidateExcelWorksheetAsync(workSheet);
        }

        private async Task<bool> ValidateExcelWorksheetAsync(ExcelWorksheet workSheet)
        {
            for (int row = 1; row <= workSheet.Dimension.Rows; row++)
            {
                workSheet.Cells[row, 12].Value = "";
                for (int col = 1; col <= 10; col++)
                {
                    workSheet.Cells[row, col].Style.Fill.SetBackground(_colorReset);
                }
            }

            bool isValid = true;
            for (int i = 2; i <= workSheet.Dimension.Rows; i++)
            {
                String[] delimiters = { ".", "/" };
                var dateString = workSheet.Cells[i, 9]?.Value?.ToString();
                var dateArray = dateString.ToString().Trim()?.Split(delimiters, StringSplitOptions.None);
                var FirstName = workSheet.Cells[i, 1]?.Value?.ToString();
                var LastName = workSheet.Cells[i, 2]?.Value?.ToString();
                var FatherName = workSheet.Cells[i, 3]?.Value?.ToString();
                var Idnp = workSheet.Cells[i, 4]?.Value?.ToString();
                var Email = workSheet.Cells[i, 5]?.Value?.ToString();
                var DepartmentColaboratorId = workSheet.Cells[i, 6]?.Value?.ToString();
                var RoleColaboratorId = workSheet.Cells[i, 7]?.Value?.ToString();
                var FunctionColaboratorId = workSheet.Cells[i, 8]?.Value?.ToString();
                var PhoneNumber = workSheet.Cells[i, 10]?.Value?.ToString();
                DateTime BirthDate;

                if (dateString == null || !Regex.IsMatch(dateString.ToString().Trim(), @"^(0[1-9]|[12][0-9]|3[01]).(0[1-9]|1[0-2]).\d{4}$"))
                {
                    workSheet.Cells[i, 12].Value += $"Câmpul obligatoriu Data nașterii nu corespunde unui format valid ZZ.LL.AAAA, valoarea trimisă este \"{workSheet.Cells[i, 9]?.Value?.ToString()}\" \n";
                    workSheet.Cells[i, 9].Style.Fill.SetBackground(_color);
                    isValid = false;
                }
                else if (dateArray != null)
                {
                    BirthDate = new DateTime(int.Parse(dateArray[2]), int.Parse(dateArray[1]), int.Parse(dateArray[0]));

                    if ((int)(DateTime.UtcNow - BirthDate).TotalDays / 365 < 18)
                    {
                        workSheet.Cells[i, 12].Value += "Vârsta minimă trebuie să fie de 18 ani \n";
                        workSheet.Cells[i, 9].Style.Fill.SetBackground(_color);
                        isValid = false;
                    }
                }
               
                if (!Regex.IsMatch(FirstName.Trim(), @"^[a-zA-ZĂăÎîȘŞșşȚŢțţÂâ]+([- ]?[a-zA-ZĂăÎîȘŞșşȚŢțţÂâ]+)*\s*$"))
                {
                    workSheet.Cells[i, 12].Value += "Câmpul obligatoriu Nume trebuie să conțină doar litere \n";
                    workSheet.Cells[i, 1].Style.Fill.SetBackground(_color);
                    isValid = false;
                }
                
                if (!Regex.IsMatch(LastName.Trim(), @"^[a-zA-ZĂăÎîȘŞșşȚŢțţÂâ]+([- ]?[a-zA-ZĂăÎîȘŞșşȚŢțţÂâ]+)*\s*$"))
                {
                    workSheet.Cells[i, 12].Value += "Câmpul obligatoriu Prenume trebuie să conțină doar litere \n";
                    workSheet.Cells[i, 2].Style.Fill.SetBackground(_color);
                    isValid = false;
                }

                if (!string.IsNullOrWhiteSpace(FatherName) && !Regex.IsMatch(FatherName.Trim(), @"^[a-zA-ZĂăÎîȘŞșşȚŢțţÂâ]+([- ]?[a-zA-ZĂăÎîȘŞșşȚŢțţÂâ]+)*\s*$"))
                {
                    workSheet.Cells[i, 12].Value += "Patronimic trebuie să conțină doar litere \n";
                    workSheet.Cells[i, 3].Style.Fill.SetBackground(_color);
                    isValid = false;
                }

                if (string.IsNullOrWhiteSpace(Idnp.Trim()) || Idnp.Trim().Length != 13 || !Regex.IsMatch(Idnp.Trim(), @"^\d+$"))
                {
                    workSheet.Cells[i, 12].Value += "Câmpul obligatoriu Idnp trebuie să conțină doar 13 cifre \n";
                    workSheet.Cells[i, 4].Style.Fill.SetBackground(_color);
                    isValid = false;
                }
                else if (_appDbContext.UserProfiles.FirstOrDefault(i => i.Idnp == Idnp && i.IsDeleted == false) != null)
                {
                    workSheet.Cells[i, 12].Value += "Idnp duplicat în sistem\n";
                    workSheet.Cells[i, 4].Style.Fill.SetBackground(_color);
                    isValid = false;
                }
                else
                {
                    isValid = await IsValidDistinctDataColumn(workSheet, (int)ExcelColumnsEnum.IdnpColumn, i);
                }

                if (string.IsNullOrWhiteSpace(Email) || !Regex.IsMatch(Email.Trim(), @"(?x)^[^\s@]+@[^\s@]+\.[^\s@]+$"))
                {
                    workSheet.Cells[i, 12].Value += "Câmpul obligatoriu Email nu corespunde formatului \n";
                    workSheet.Cells[i, 5].Style.Fill.SetBackground(_color);
                    isValid = false;
                }
                else if (_appDbContext.UserProfiles.FirstOrDefault(i => i.Email == Email && i.IsDeleted == false) != null)
                {
                    workSheet.Cells[i, 12].Value += "Email duplicat în sistem\n";
                    workSheet.Cells[i, 5].Style.Fill.SetBackground(_color);
                    isValid = false;
                }
                else
                {
                    isValid = await IsValidDistinctDataColumn(workSheet, (int)ExcelColumnsEnum.EmailColumn, i);
                }

                if (string.IsNullOrWhiteSpace(DepartmentColaboratorId) || !Regex.IsMatch(DepartmentColaboratorId.ToString().Trim(), @"^\d+$"))
                {
                    workSheet.Cells[i, 12].Value += "Câmpul obligatoriu ID Departament trebuie să conțină doar cifre \n";
                    workSheet.Cells[i, 6].Style.Fill.SetBackground(_color);
                    isValid = false;
                }
                else if (_appDbContext.Departments.FirstOrDefault(i => i.ColaboratorId == int.Parse(DepartmentColaboratorId)) == null)
                {
                    workSheet.Cells[i, 12].Value += "ID Departament nu există în baza de date\n";
                    workSheet.Cells[i, 6].Style.Fill.SetBackground(_color);
                    isValid = false;
                }

                if (string.IsNullOrWhiteSpace(RoleColaboratorId) || !Regex.IsMatch(RoleColaboratorId.ToString().Trim(), @"^\d+$"))
                {
                    workSheet.Cells[i, 12].Value += "Câmpul obligatoriu ID Rol trebuie să conțină doar cifre \n";
                    workSheet.Cells[i, 7].Style.Fill.SetBackground(_color);
                    isValid = false;
                }
                else if (_appDbContext.Roles.FirstOrDefault(i => i.ColaboratorId == int.Parse(RoleColaboratorId)) == null)
                {
                    workSheet.Cells[i, 12].Value += "ID Rol nu există în baza de date\n";
                    workSheet.Cells[i, 7].Style.Fill.SetBackground(_color);
                    isValid = false;
                }

                if (string.IsNullOrWhiteSpace(FunctionColaboratorId) || !Regex.IsMatch(FunctionColaboratorId.ToString().Trim(), @"^\d+$"))
                {
                    workSheet.Cells[i, 12].Value += "Câmpul obligatoriu ID Funcție trebuie să conțină doar cifre \n";
                    workSheet.Cells[i, 8].Style.Fill.SetBackground(_color);
                    isValid = false;
                }
                else if (_appDbContext.EmployeeFunctions.FirstOrDefault(i => i.ColaboratorId == int.Parse(FunctionColaboratorId)) == null)
                {
                    workSheet.Cells[i, 12].Value += "ID Funcție nu există în baza de date\n";
                    workSheet.Cells[i, 8].Style.Fill.SetBackground(_color);
                    isValid = false;
                }

                if (string.IsNullOrWhiteSpace(PhoneNumber) || !Regex.IsMatch(PhoneNumber.Trim(), @"(?x)^\s*\+373[0-9]{8}\s*$") || PhoneNumber.Trim().Length != 12)
                {
                    workSheet.Cells[i, 12].Value += "Câmpul obligatoriu Nr. telefon nu corespunde formatului +373xxxxxxxx\n";
                    workSheet.Cells[i, 10].Style.Fill.SetBackground(_color);
                    isValid = false;
                }
            }

            return isValid;
        }

        private async Task<bool> IsValidDistinctDataColumn(ExcelWorksheet workSheet, int column, int i)
        {
            var cells = workSheet.Cells;

            //Get new dictionary with keys of excel data
            var dictionary = cells
                .GroupBy(c => new { c.Start.Row, c.Start.Column })
                .ToDictionary(
                    rcg => new KeyValuePair<int, int>(rcg.Key.Row, rcg.Key.Column),
                    rcg => cells[rcg.Key.Row, rcg.Key.Column].Value);

            //Get needed column
            var columnDictionary = dictionary.Where(x => x.Key.Value == column);

            //Find repeated items by groups
            var repeatedItemsGroups = columnDictionary
                .GroupBy(x => x.Value)
                .Where(x => x.Count() > 1);

            var itemsGroups = repeatedItemsGroups as IGrouping<object, KeyValuePair<KeyValuePair<int, int>, object>>[] ?? repeatedItemsGroups.ToArray();
            await SetInvalidCells(workSheet, itemsGroups, column, i);

            return !itemsGroups.Any();
        }

        private async Task SetInvalidCells(ExcelWorksheet workSheet, IGrouping<object, KeyValuePair<KeyValuePair<int, int>, object>>[] itemsGroups, int column, int i)
        {
            foreach (var items in itemsGroups)
            {
                //foreach (var item in items)
                //{
                    workSheet.Cells[i, 12].Value += await GetErrorMessage(column);
                    workSheet.Cells[i, column].Style.Fill.SetBackground(_color);
                //}
            }
        }

        private async Task<string> GetErrorMessage(int column)
        {
            return column switch
            {
                (int) ExcelColumnsEnum.IdnpColumn => "Idnp duplicat \n",
                (int) ExcelColumnsEnum.EmailColumn => "Email duplicat \n",
                _ => string.Empty
            };
        }
    }
}
