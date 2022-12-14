using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.Application.TestQuestions.GenerateTestQuestions;
using CODWER.RERU.Evaluation.Application.Tests.AddTest;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using CVU.ERP.Notifications.Email;
using CVU.ERP.Notifications.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Entities;

namespace CODWER.RERU.Evaluation.Application.Tests.AddEvaluations
{
    public class AddEvaluationsCommandHandler : IRequestHandler<AddEvaluationsCommand, List<int>>
    {
        private readonly IMediator _mediator;
        private readonly AppDbContext _appDbContext;
        private readonly INotificationService _notificationService;
        private readonly IInternalNotificationService _internalNotificationService;
        private readonly ILoggerService<AddEvaluationsCommandHandler> _loggerService;
        private readonly List<int> _testsIds;

        public AddEvaluationsCommandHandler(IMediator mediator, 
            AppDbContext appDbContext, 
            INotificationService notificationService, 
            IInternalNotificationService internalNotificationService, 
            ILoggerService<AddEvaluationsCommandHandler> loggerService)
        {
            _mediator = mediator;
            _appDbContext = appDbContext;
            _notificationService = notificationService;
            _internalNotificationService = internalNotificationService;
            _loggerService = loggerService;

            _testsIds = new List<int>();
        }

        public async Task<List<int>> Handle(AddEvaluationsCommand request, CancellationToken cancellationToken)
        {
            var processId = request.ProcessId;

            foreach (var evaluatorId in request.EvaluatorIds)
            {
                foreach (var userProfileId in request.UserProfileIds)
                {
                    var addCommand = new AddTestCommand
                    {
                        Data = new AddEditTestDto
                        {
                            UserProfileId = userProfileId,
                            EvaluatorId = evaluatorId,
                            ShowUserName = request.ShowUserName,
                            TestTemplateId = request.TestTemplateId,
                            EventId = request.EventId,
                            TestStatus = request.TestStatus,
                            ProgrammedTime = request.ProgrammedTime,
                            SolicitedTime = request.SolicitedTime,
                            LocationId = request.LocationId
                        }
                    };

                    var testId = await _mediator.Send(addCommand);

                    _testsIds.Add(testId);

                    var generateCommand = new GenerateTestQuestionsCommand
                    {
                        TestId = testId
                    };

                    await _mediator.Send(generateCommand);

                    await UpdateProcesses(processId);

                    await SendEmailNotificationForEvaluat(testId, addCommand);

                    await LogAction(testId);
                }
            }

            await CloseProcess(processId);

            await SendEmailNotificationForEvaluator(request.EvaluatorIds, request.UserProfileIds, request.TestTemplateId);

            return _testsIds;
        }

        private async Task UpdateProcesses(int processId)
        {
            var process = _appDbContext.Processes.First(x => x.Id == processId);
            process.Done++;

            await _appDbContext.SaveChangesAsync();
        }

        private async Task CloseProcess(int processId)
        {
            var process = _appDbContext.Processes.First(x => x.Id == processId);
            process.IsDone = true;

            await _appDbContext.SaveChangesAsync();
        }

        private async Task LogAction(int testId)
        {
            var test = await _appDbContext.Tests
                .Include(x => x.UserProfile)
                .Include(x => x.Evaluator)
                .Include(x => x.TestTemplate)
                .FirstOrDefaultAsync(x => x.Id == testId);

            await _loggerService.Log(LogData.AsEvaluation($@"Evaluatorul ""{test.Evaluator.FullName}"" a fost atașat/ă ca evaluator pentru ""{test.UserProfile.FullName}"" la evaluarea ``{test.TestTemplate.Name}``"));
        }

        #region EvaluatEmail
        private async Task<Unit> SendEmailNotificationForEvaluat(int testId, AddTestCommand addTestCommand)
        {
            var test = await _appDbContext.Tests
                .Include(x => x.Event)
                .ThenInclude(x => x.EventLocations)
                .ThenInclude(x => x.Location)
                .Include(x => x.UserProfile)
                .Include(x => x.TestTemplate)
                .FirstOrDefaultAsync(x => x.Id == testId);

            await SendEmail(test.UserProfile, await GetEmailContentForEvaluat(test.TestTemplate.Name, addTestCommand));

            return Unit.Value;
        }

        private async Task<string> GetEmailContentForEvaluat(string testName, AddTestCommand addTestCommand)
        {
            var content = $@"<p>sunteți invitat/ă la evaluarea ""{testName}""</p>";

            if (addTestCommand.Data.SolicitedTime.HasValue)
            {
                content += $@"<p>Data și ora: ""{addTestCommand.Data.SolicitedTime.Value:dd/MM/yyyy HH:mm}"".</p>";
            }

            if (addTestCommand.Data.LocationId.HasValue)
            {
                var location = await _appDbContext.Locations.FirstAsync(x => x.Id == addTestCommand.Data.LocationId);

                content += $@"<p> Locatia: ""{location.Address ?? "Nici o adresă asignata"}"", ""{location.Name ?? "Nici o locație asignată"}"" </p>";

                if (string.IsNullOrEmpty(location.Address))
                {
                     content += $@"<p> ""{location.Description}""</p>";
                }

                content += "<p> Prezența fizică este obligatorie. </p>";
            }

            return content;
        }


        #endregion

        #region EvaluatorEmail
        private async Task<Unit> SendEmailNotificationForEvaluator(List<int> evaluatorIds, List<int> userProfileIds, int testTemplateId)
        {
            var testTemplate = await _appDbContext.TestTemplates.FirstOrDefaultAsync(x => x.Id == testTemplateId);

            foreach (var evaluatorId in evaluatorIds)
            {
                var evaluator = await _appDbContext.UserProfiles
                    .FirstOrDefaultAsync(x => x.Id == evaluatorId);

                await SendEmail(evaluator, await GetEmailEvaluatorContent(userProfileIds, testTemplate.Name));

                await _internalNotificationService.AddNotification(evaluatorId, NotificationMessages.YouWereInvitedToTestAsEvaluator);
            }

            return Unit.Value;
        }

        private async Task<string> GetEmailEvaluatorContent(List<int> userProfileIds, string testName)
        {
            var content = $@"<p style=""font-size: 22px; font-weight: 300;"">sunteți invitat/ă la evaluarea ""{testName}"" pentru: </p>";

            foreach (var userProfileId in userProfileIds.Select((value, i) => new { i, value }))
            {
                var userProfile = await _appDbContext.UserProfiles.FirstOrDefaultAsync(x => x.Id == userProfileId.value);

                content += $@"<div style=""font-size: 22px; font-weight: 300; text-align:center;"">{userProfileId.i + 1}. {userProfile.FullName}</div>";
            }

            content += $@"<p style=""font-size: 22px; font-weight: 300;"">în rol de evaluator.</p>";

            return content;
        }


        #endregion

        private async Task SendEmail(UserProfile user, string message)
        {
            await _notificationService.PutEmailInQueue(new QueuedEmailData
            {
                Subject = "Invitație la evaluare",
                To = user.Email,
                HtmlTemplateAddress = "Templates/Evaluation/EmailNotificationTemplate.html",
                ReplacedValues = new Dictionary<string, string>()
                {
                    { "{user_name}", user.FullName },
                    { "{email_message}",  message }
                }
            });
        }
       
    }
}
