using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.Application.TestQuestions.GenerateTestQuestions;
using CODWER.RERU.Evaluation.Application.Tests.AddTest;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using CVU.ERP.Common.DataTransferObjects.Config;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using CVU.ERP.Notifications.Email;
using CVU.ERP.Notifications.Services;
using CVU.ERP.StorageService;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OfficeOpenXml;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System;
using System.Collections.Generic;
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
        private readonly IInternalNotificationService _internalNotificationService;
        private readonly ILoggerService<AddTestsCommandHandler> _loggerService;
        private readonly List<int> _testsIds;

        public AddTestsCommandHandler(
            IMediator mediator,
            AppDbContext appDbContext,
            INotificationService notificationService,
            IOptions<PlatformConfig> options,
            IStorageFileService storageFileService, IInternalNotificationService internalNotificationService, ILoggerService<AddTestsCommandHandler> loggerService)
        {
            _mediator = mediator;
            _appDbContext = appDbContext;
            _notificationService = notificationService;
            _storageFileService = storageFileService;
            _internalNotificationService = internalNotificationService;
            _loggerService = loggerService;
            _platformConfig = options.Value;

            _testsIds = new List<int>();

            _excelWorksheet = _excelPackage.Workbook.Worksheets.Add("Sheet1");

            _excelWorksheet.Cells[1, 1].Value = "Name";
            _excelWorksheet.Cells[1, 2].Value = "Idnp";
            _excelWorksheet.Cells[1, 3].Value = "Email";
            _excelWorksheet.Cells[1, 4].Value = "Result";
            _excelWorksheet.Cells[1, 5].Value = "Error";
        }

        public async Task<List<int>> Handle(AddTestsCommand request, CancellationToken cancellationToken)
        {
            var processId = request.ProcessId;
            var tasks = new List<Task>();

            for (int userProfileIndex = 0; userProfileIndex < request.UserProfileIds.Count; userProfileIndex++)
            {
                var myHashGroupKey = Guid.NewGuid().ToString();
                int userProfileIndexCopy = userProfileIndex;
                var myHashGroupKeyCopy = myHashGroupKey;

                if (request.EvaluatorIds.Any())
                {
                    for (int evaluatorIndex = 0; evaluatorIndex < request.EvaluatorIds.Count; evaluatorIndex++)
                    {
                        int evaluatorIndexCopy = evaluatorIndex;
                        tasks.Add(Task.Run(() => HandleTask(request, userProfileIndexCopy, evaluatorIndexCopy, processId, myHashGroupKeyCopy)));
                    }
                }
                else
                {
                    tasks.Add(Task.Run(() => HandleTask(request, userProfileIndexCopy, null, processId, myHashGroupKeyCopy)));
                }
            }

            await WaitTasks(Task.WhenAll(tasks));

            tasks.Clear();

            await SaveExcelFile(processId, _excelPackage);

            return _testsIds;
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

        private async Task HandleTask(AddTestsCommand request, int i, int? j, int processId, string myHashGroupKey)
        {
            var addCommand = await GetCommand(request, i, j, myHashGroupKey);

            try
            {
                var testId = await _mediator.Send(addCommand);

                _testsIds.Add(testId);

                var generateCommand = new GenerateTestQuestionsCommand
                {
                    TestId = testId,
                };

                await _mediator.Send(generateCommand);

                await UpdateProcesses(processId);

                await GenerateExcelResult(i, addCommand.Data.UserProfileId, true, string.Empty);

                await SendEmailNotificationToEvaluat(testId);

                if (addCommand.Data.EvaluatorId != null)
                {
                    await SendEmailNotificationToEvaluator(addCommand.Data.EvaluatorId, request.TestTemplateId);
                }

                await LogAction(testId);
            }
            catch (Exception e)
            {
                await GenerateExcelResult(i, addCommand.Data.UserProfileId, false, e.ToString());

                Console.WriteLine(e);
            }
        }

        private async Task<AddTestCommand> GetCommand(AddTestsCommand request, int userProfileIndex, int? evaluatorIndex, string myHashGroupKey)
        {
            return new AddTestCommand
            {
                Data = new AddEditTestDto
                {
                    UserProfileId = request.UserProfileIds[userProfileIndex],
                    EvaluatorId = evaluatorIndex != null ? request.EvaluatorIds[(int)evaluatorIndex] : null,
                    ShowUserName = request.ShowUserName,
                    TestTemplateId = request.TestTemplateId,
                    HashGroupKey = myHashGroupKey,
                    EventId = request.EventId,
                    TestStatus = request.TestStatus,
                    ProgrammedTime = request.ProgrammedTime
                }
            };
        }

        private async Task UpdateProcesses(int processId)
        {
            await using var db = _appDbContext.NewInstance();

            var process = db.Processes.First(x => x.Id == processId);
            process.Done++;

            await db.SaveChangesAsync();
        }

        private async Task GenerateExcelResult(int i, int userProfileId, bool result, string error)
        {
            await using (var xd = _appDbContext.NewInstance())
            {
                
            }
            await using var db = _appDbContext.NewInstance();

            var userProfile = db.UserProfiles.FirstOrDefault(x => x.Id == userProfileId);

            _excelWorksheet.Cells[i + 2, 1].Value = userProfile.FullName;
            _excelWorksheet.Column(1).Width = 25;

            _excelWorksheet.Cells[i + 2, 2].Value = userProfile?.Idnp;
            _excelWorksheet.Column(2).Width = 25;

            _excelWorksheet.Cells[i + 2, 3].Value = userProfile?.Email;
            _excelWorksheet.Column(3).Width = 45;

            _excelWorksheet.Cells[i + 2, 4].Value = result ? "Adaugat" : "Nereusit";
            _excelWorksheet.Column(4).Width = 25;

            _excelWorksheet.Cells[i + 2, 5].Value = error;
            _excelWorksheet.Column(5).Width = 25;
        }

        private async Task LogAction(int testId)
        {
            await using var db = _appDbContext.NewInstance();

            var test = await db.Tests
                .Include(x => x.UserProfile)
                .Include(x => x.Evaluator)
                .Include(x => x.TestTemplate)
                .FirstOrDefaultAsync(x => x.Id == testId);

            await _loggerService.Log(LogData.AsEvaluation($@"Utilizatorul ""{test.UserProfile.FullName}"" a fost atașat/ă la testul ""{test.TestTemplate.Name}"" data: ""{test.ProgrammedTime:dd/MM/yyyy HH:mm}"""));
        }

        private async Task SaveExcelFile(int processId, ExcelPackage package)
        {
            await using var db = _appDbContext.NewInstance();

            var excelFile = await GetExcelFile(package);

            var fileId = await _storageFileService.AddFile(excelFile.Name, FileTypeEnum.procesfile, excelFile.ContentType, excelFile.Content);

            var process = db.Processes.First(x => x.Id == processId);

            process.FileId = fileId;
            process.IsDone = true;

            await db.SaveChangesAsync();
        }

        private async Task<FileDataDto> GetExcelFile(ExcelPackage package)
        {
            const string fileName = "AddTestResult.xlsx";
            var streamBytesArray = package.GetAsByteArray();

            return FileDataDto.GetExcel(fileName, streamBytesArray);
        }


        #region EvaluatorMail
        private async Task SendEmailNotificationToEvaluator(int? evaluatorId, int testTemplateId)
        {
            if (evaluatorId == null) return;

            await using var db = _appDbContext.NewInstance();
            var user = await db.UserProfiles.FirstOrDefaultAsync(x => x.Id == evaluatorId);

            await _internalNotificationService.AddNotification((int)evaluatorId, NotificationMessages.YouWereInvitedToTestAsEvaluator);

            await SendEmail(user, await GetEvaluatorMessage(testTemplateId));
        }

        private async Task<string> GetEvaluatorMessage(int testTemplateId)
        {
            await using var db = _appDbContext.NewInstance();

            var testTemplate = await db.TestTemplates.FirstAsync(x => x.Id == testTemplateId);

            return $@"<p style=""font-size: 22px; font-weight: 300;"">sunteți invitat/ă la testul ""{testTemplate.Name}"" în rol de evaluator.</p>";
        }
        #endregion

        #region EvaluatMail
        private async Task SendEmailNotificationToEvaluat(int testId)
        {
            await using var db = _appDbContext.NewInstance();
            var test = await db.Tests
                .Include(x => x.TestTemplate)
                .FirstOrDefaultAsync(x => x.Id == testId);

            var user = await db.UserProfiles.FirstOrDefaultAsync(x => x.Id == test.UserProfileId);

            await _internalNotificationService.AddNotification(test.UserProfileId, NotificationMessages.YouHaveNewProgrammedTest);

            await SendEmail(user, await GetEvaluatMessage(test));
        }

        private async Task<string> GetEvaluatMessage(Test test)
        {
            var content = $@"<p style=""font-size: 22px; font-weight: 300;"">sunteți invitat/ă la testul ""{test.TestTemplate.Name}"", în rol de candidat.</p>";

            content += test.EventId != null
                ? $@" <p style=""font-size: 22px; font-weight: 300;"">Testul va avea loc în perioada: {test.ProgrammedTime.ToString("dd/MM/yyyy HH:mm")}-{test.EndProgrammedTime?.ToString("dd/MM/yyyy HH:mm")}.</p>"
                : $@" <p style=""font-size: 22px; font-weight: 300;"">Testul va avea loc pe data: {test.ProgrammedTime.ToString("dd/MM/yyyy HH:mm")}.</p>";

            content += $@"<p>Pentru a accesa testul programat pe Dvs, urmați pașii:</p>
                      <p> 1.Logați-vă pe pagina {_platformConfig.BaseUrl}</p>
                      <p> 2.Click pe butonul ""Evaluare"" </p>
                      <p> 3.Click pe butonul ""Activitățile mele"" </p>
                      <p> 4.Din meniul din stânga, alegeți opțiunea TESTE.</p>
                      <p> 5.Pentru a începe testul, click ""Începe Testul"" din partea dreaptă de jos a paginii </p>
                      <p> 6.Bifați acceptarea regulilor, selectează ""Acceptă"", și click butonul ""Începe"" </p>";

            return content;
        }
        #endregion

        private async Task SendEmail(UserProfile user, string message)
        {
            await _notificationService.PutEmailInQueue(new QueuedEmailData
            {
                Subject = "Invitație la test",
                To = user.Email,
                HtmlTemplateAddress = "Templates/Evaluation/EmailNotificationTemplate.html",
                ReplacedValues = new Dictionary<string, string>()
                {
                    { "{user_name}", user.FullName },
                    { "{email_message}",message }
                }
            });
        }
    }
}
