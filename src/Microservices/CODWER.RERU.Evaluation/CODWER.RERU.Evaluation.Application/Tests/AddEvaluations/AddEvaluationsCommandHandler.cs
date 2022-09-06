using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Tests.AddEvaluations
{
    public class AddEvaluationsCommandHandler : IRequestHandler<AddEvaluationsCommand, List<int>>
    {
        private readonly IMediator _mediator;
        private readonly AppDbContext _appDbContext;
        private readonly INotificationService _notificationService;
        private readonly IInternalNotificationService _internalNotificationService;
        private readonly ILoggerService<AddEvaluationsCommandHandler> _loggerService;

        public AddEvaluationsCommandHandler(IMediator mediator, AppDbContext appDbContext, INotificationService notificationService, IInternalNotificationService internalNotificationService, ILoggerService<AddEvaluationsCommandHandler> loggerService)
        {
            _mediator = mediator;
            _appDbContext = appDbContext;
            _notificationService = notificationService;
            _internalNotificationService = internalNotificationService;
            _loggerService = loggerService;
        }

        public async Task<List<int>> Handle(AddEvaluationsCommand request, CancellationToken cancellationToken)
        {
            int testId = 0;
            var testsIds = new List<int>();

            for (int i = 0; i < request.EvaluatorIds.Count; i++)
            {
                for (int j = 0; j < request.UserProfileIds.Count; j++)
                {
                    var addCommand = new AddTestCommand
                    {
                        Data = new AddEditTestDto
                        {
                            UserProfileId = request.UserProfileIds[j],
                            EvaluatorId = request.EvaluatorIds[i],
                            ShowUserName = request.ShowUserName,
                            TestTemplateId = request.TestTemplateId,
                            EventId = request.EventId,
                            TestStatus = request.TestStatus,
                            ProgrammedTime = request.ProgrammedTime
                        }
                    };

                    testId = await _mediator.Send(addCommand);

                    testsIds.Add(testId);

                    var generateCommand = new GenerateTestQuestionsCommand
                    {
                        TestId = testId
                    };

                    await _mediator.Send(generateCommand);
                    await LogAction(testId);
                }
            }

            await SendEmailNotification(request.EvaluatorIds, request.UserProfileIds, request.TestTemplateId);

            return testsIds;
        }

        private async Task<Unit> SendEmailNotification(List<int> evaluatorIds, List<int> userProfileIds, int testTemplateId)
        {
            var testTemplate = await _appDbContext.TestTemplates.FirstOrDefaultAsync(x => x.Id == testTemplateId);

            foreach (var evaluatorId in evaluatorIds)
            {
                var evaluator = await _appDbContext.UserProfiles
                    .FirstOrDefaultAsync(x => x.Id == evaluatorId);

                await _notificationService.PutEmailInQueue(new QueuedEmailData
                {
                    Subject = "Invitație la evaluare",
                    To = evaluator.Email,
                    HtmlTemplateAddress = "Templates/Evaluation/EmailNotificationTemplate.html",
                    ReplacedValues = new Dictionary<string, string>()
                    {
                        { "{user_name}", evaluator.FullName },
                        { "{email_message}",  await GetEmailContent(userProfileIds, testTemplate.Name)}
                    }
                });

                await _internalNotificationService.AddNotification(evaluatorId, NotificationMessages.YouWereInvitedToTestAsEvaluator);
            }

            return Unit.Value;
        }

        private async Task<string> GetEmailContent(List<int> userProfileIds, string testName)
        {
            var userNames = new List<string>();

            foreach (var userProfileId in userProfileIds)
            {
                var userProfile = await _appDbContext.UserProfiles.FirstOrDefaultAsync(x => x.Id == userProfileId);

                userNames.Add(userProfile.FullName);
            }

            return $@"<p style=""font-size: 22px; font-weight: 300;"">sunteți invitat/ă la evaluarea ""{testName}"" pentru {string.Join(",", userNames.ToArray())} în rol de evaluator.</p>";
        }

        private async Task LogAction(int testId)
        {
            var test = await _appDbContext.Tests
                .Include(x => x.UserProfile)
                .Include(x => x.Evaluator)
                .Include(x => x.TestTemplate)
                .FirstOrDefaultAsync(x => x.Id == testId);

            await _loggerService.Log(LogData.AsEvaluation($"The evaluator {test.Evaluator.FirstName} {test.Evaluator.LastName} was invited to evaluate {test.UserProfile.FirstName} {test.UserProfile.LastName} by {test.TestTemplate.Name} evaluation"));
        }
    }
}
