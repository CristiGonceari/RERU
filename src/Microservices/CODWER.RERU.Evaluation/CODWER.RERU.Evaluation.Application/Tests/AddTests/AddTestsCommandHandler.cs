using CODWER.RERU.Evaluation.Application.Models;
using CODWER.RERU.Evaluation.Application.TestQuestions.GenerateTestQuestions;
using CODWER.RERU.Evaluation.Application.Tests.AddTest;
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Notifications.Email;
using CVU.ERP.Notifications.Enums;
using CVU.ERP.Notifications.Services;
using CVU.ERP.StorageService;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OfficeOpenXml;
using RERU.Data.Entities;
using RERU.Data.Entities.StaticExtensions;
using RERU.Data.Persistence.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FileTypeEnum = CVU.ERP.StorageService.Entities.FileTypeEnum;

namespace CODWER.RERU.Evaluation.Application.Tests.AddTests
{
    public class AddTestsCommandHandler : IRequestHandler<AddTestsCommand, List<int>>
    {
        private readonly IMediator _mediator;
        private readonly AppDbContext _appDbContext;
        private readonly INotificationService _notificationService;
        private readonly PlatformConfig _platformConfig;
        private readonly IStorageFileService _storageFileService;
        private readonly ExcelPackage _excelPackage = new();
        private readonly ExcelWorksheet _excelWorksheet;

        public AddTestsCommandHandler(
            IMediator mediator,
            AppDbContext appDbContext,
            INotificationService notificationService,
            IOptions<PlatformConfig> options,
            IStorageFileService storageFileService)
        {
            _mediator = mediator;
            _appDbContext = appDbContext;
            _notificationService = notificationService;
            _storageFileService = storageFileService;
            _platformConfig = options.Value;
            _excelWorksheet = _excelPackage.Workbook.Worksheets.Add("Sheet1");

            _excelWorksheet.Cells[1, 1].Value = "Name";
            _excelWorksheet.Cells[1, 2].Value = "Idnp";
            _excelWorksheet.Cells[1, 3].Value = "Email";
            _excelWorksheet.Cells[1, 4].Value = "Result";
            _excelWorksheet.Cells[1, 5].Value = "Error";
        }

        public async Task<List<int>> Handle(AddTestsCommand request, CancellationToken cancellationToken)
        {
            int testId = 0;
            var testsIds = new List<int>();

            var processId = request.ProcessId;

            for (int i = 0; i < request.UserProfileId.Count; i++)
            {
                var addCommand = new AddTestCommand
                {
                    Data = new AddEditTestDto
                    {
                        UserProfileId = request.UserProfileId[i],
                        EvaluatorId = request.EvaluatorId,
                        ShowUserName = request.ShowUserName,
                        TestTemplateId = request.TestTemplateId,
                        EventId = request.EventId,
                        TestStatus = request.TestStatus,
                        ProgrammedTime = request.ProgrammedTime
                    }
                };

                try
                {
                    testId = await _mediator.Send(addCommand);

                    testsIds.Add(testId);

                    var generateCommand = new GenerateTestQuestionsCommand
                    {
                        TestId = testId
                    };

                    await _mediator.Send(generateCommand);

                    await UpdateProcesses(processId);

                    await GenerateExcelResult(i, addCommand.Data.UserProfileId, true, string.Empty, _excelWorksheet);

                    await SendEmailNotification(addCommand, null, testId);

                }
                catch (Exception e)
                {
                    await GenerateExcelResult(i, addCommand.Data.UserProfileId, false, e.ToString(), _excelWorksheet);

                    Console.WriteLine(e);
                }
            }

            await SaveExcelFile(processId, _excelPackage);

            await SendEmailNotification(null, request, testId);

            return testsIds;
        }

        private async Task<Unit> SendEmailNotification(AddTestCommand testCommand, AddTestsCommand request, int testId)
        {
            var path = new FileInfo("PdfTemplates/EmailNotificationTemplate.html").FullName;
            var template = await File.ReadAllTextAsync(path);

            var user = new UserProfile();
            var test = await _appDbContext.Tests
                .Include(x => x.TestTemplate)
                .FirstOrDefaultAsync(x => x.Id == testId);

            if (testCommand != null)
            {
                user = await _appDbContext.UserProfiles.FirstOrDefaultAsync(x => x.Id == testCommand.Data.UserProfileId);
                template = template.Replace("{email_message}", await GetTableContent(test, true));
            }
            else
            {
                if (request.EvaluatorId != null)
                {
                    user = await _appDbContext.UserProfiles.FirstOrDefaultAsync(x => x.Id == request.EvaluatorId);
                    template = template.Replace("{email_message}", await GetTableContent(test, false));
                }
                else
                {
                    return Unit.Value;
                }
            }

            template = template.Replace("{user_name}", user.FirstName + " " + user.LastName);

            var emailData = new EmailData()
            {
                subject = "Invitație la test",
                body = template,
                from = "Do Not Reply",
                to = user.Email
            };

            await _notificationService.Notify(emailData, NotificationType.Both);

            return Unit.Value;
        }

        private async Task<string> GetTableContent(Test item, bool evaluat)
        {
            var content = string.Empty;

            if (evaluat)
            {
                content += $@"<p style=""font-size: 22px; font-weight: 300;"">Ați fost invitat la testul ""{item.TestTemplate.Name}"" în rol de candidat.</p>
                          <p style=""font-size: 22px; font-weight: 300;""> Testul va avea loc pe data: {item.ProgrammedTime.ToString("dd/MM/yyyy")}.</p> 
                          <p>Pentru a accesa testul programat pe Dvs, urmati pasii:</p>
                          <p>1. Logati- va pe pagina {_platformConfig.BaseUrl}</p>
                          <p>2.Click pe butonul ""Evaluare"" </p>
                          <p> 3.Click pe butonul ""Activitatile mele"" </p>
                          <p> 4.Din meniul din stanga alegeti optiunea ""Eveniment"", daca testul a fost programat cu eveniment </p>
                          <p> 5.Din meniul din stanga alegeti optiunea ""Teste"", daca testul a fost programat fara eveniment </p>
                          <p> 6.Pentru a incepe testul, click ""Incepe Testul"" din partea dreapta de jos a paginii </p>
                          <p> 7.Bifati acceptarea regulilor, selecteaza ""Accepta"", si click butonul ""Incepe"" </p>";
            }
            else
            {
                content += $@"<p style=""font-size: 22px; font-weight: 300;"">Ați fost invitat la testul ""{item.TestTemplate.Name}"" în rol de evaluator.</p>";
            }

            return content;
        }

        private async Task UpdateProcesses(int processId)
        {
            var process = _appDbContext.Processes.First(x => x.Id == processId);
            process.Done++;

            await _appDbContext.SaveChangesAsync();
        }

        private async Task GenerateExcelResult(int i, int userProfileId, bool result, string error, ExcelWorksheet workSheet)
        {
            var userProfile = _appDbContext.UserProfiles.FirstOrDefault(x => x.Id == userProfileId);

            workSheet.Cells[i + 2, 1].Value = userProfile.GetFullName();
            workSheet.Column(1).Width = 25;

            workSheet.Cells[i + 2, 2].Value = userProfile?.Idnp;
            workSheet.Column(2).Width = 25;

            workSheet.Cells[i + 2, 3].Value = userProfile?.Email;
            workSheet.Column(3).Width = 45;

            workSheet.Cells[i + 2, 4].Value = result ? "Adaugat" : "Nereusit";
            workSheet.Column(4).Width = 25;

            workSheet.Cells[i + 2, 5].Value = error;
            workSheet.Column(5).Width = 25;
        }

        private async Task<FileDataDto> GetExcelFile(ExcelPackage package)
        {
            const string fileName = "AddTestResult.xlsx";
            var streamBytesArray = package.GetAsByteArray();

            return new FileDataDto
            {
                Content = streamBytesArray,
                Name = fileName,
                ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            };
        }

        private async Task SaveExcelFile(int processId, ExcelPackage package)
        {
            var excelFile = await GetExcelFile(package);

            var fileId = await _storageFileService.AddFile(excelFile.Name, FileTypeEnum.procesfile, excelFile.ContentType, excelFile.Content);

            var process = _appDbContext.Processes.First(x => x.Id == processId);

            process.FileId = fileId;
            process.IsDone = true;

            await _appDbContext.SaveChangesAsync();
        }
    }
}
