using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Application.VerificationTests.AutoCheckTestScore;
using CODWER.RERU.Evaluation.Application.VerificationTests.AutoVerificationTestQuestions;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Notifications.Email;
using CVU.ERP.Notifications.Enums;
using CVU.ERP.Notifications.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.Tests.FinalizeTest
{
    public class FinalizeTestCommandHandler : IRequestHandler<FinalizeTestCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMediator _mediator;
        private readonly INotificationService _notificationService;
        private readonly IInternalNotificationService _internalNotificationService;

        public FinalizeTestCommandHandler(AppDbContext appDbContext, IMediator mediator, INotificationService notificationService, IInternalNotificationService internalNotificationService)
        {
            _appDbContext = appDbContext;
            _mediator = mediator;
            _notificationService = notificationService;
            _internalNotificationService = internalNotificationService;
        }

        public async Task<Unit> Handle(FinalizeTestCommand request, CancellationToken cancellationToken)
        {
            var testToFinalize = await _appDbContext.Tests
                .Include(x => x.TestTemplate)
                .FirstAsync(x => x.Id == request.TestId);

            testToFinalize.TestStatus = TestStatusEnum.Terminated;

            await _appDbContext.SaveChangesAsync();

            await _mediator.Send(new AutoVerificationTestQuestionsCommand { TestId = request.TestId });

            if (testToFinalize.TestQuestions.All(x => x.QuestionUnit.QuestionType == QuestionTypeEnum.OneAnswer || x.QuestionUnit.QuestionType == QuestionTypeEnum.MultipleAnswers))
            {
                await _mediator.Send(new AutoCheckTestScoreCommand { TestId = request.TestId });
                testToFinalize.TestStatus = TestStatusEnum.Verified;
                await _appDbContext.SaveChangesAsync();

                await SendEmailNotification(testToFinalize, true);
            }

            await SendEmailNotification(testToFinalize, false);

            return Unit.Value;
        }

        private async Task<Unit> SendEmailNotification(Test testToFinalize, bool autoCheck)
        {
            var path = new FileInfo("PdfTemplates/EmailNotificationTemplate.html").FullName;
            var template = await File.ReadAllTextAsync(path);

            var eventEvaluators = await _appDbContext.EventEvaluators
                    .Include(x => x.Evaluator)
                    .Include(x => x.Event)
                    .Where(x => x.EventId == testToFinalize.EventId)
                    .ToListAsync();

            var candidate = await _appDbContext.UserProfiles.FirstOrDefaultAsync(x => x.Id == testToFinalize.UserProfileId);

            if (autoCheck)
            {
                await Send(candidate, testToFinalize, template, autoCheck);

                await _internalNotificationService.AddNotification(candidate.Id, NotificationMessages.YourTestWasVerified);
            }
            else 
            {
                foreach (var evaluator in eventEvaluators)
                {
                    var userTests = _appDbContext.EventUsers
                        .Include(x => x.Event)
                        .Include(x => x.UserProfile)
                            .ThenInclude(x => x.Tests)
                        .Where(x => x.EventId == testToFinalize.EventId)
                        .All(x => x.UserProfile.TestsWithEvaluator
                                        .Where(x => x.EventId == testToFinalize.EventId)
                                        .All(t => t.TestStatus == TestStatusEnum.Terminated) &&
                                    x.UserProfile.TestsWithEvaluator
                                        .Where(x => x.EventId == testToFinalize.EventId)
                                        .Any(t => t.UserProfileId == x.UserProfileId));

                    if (userTests)
                    {
                        await Send(evaluator.Evaluator, testToFinalize, template, autoCheck);

                        await _internalNotificationService.AddNotification(evaluator.EvaluatorId, NotificationMessages.AllCandidatesFinishedTest);
                    }
                    else
                    {
                        return Unit.Value;
                    }
                }
            }

            return Unit.Value;
        }

        private async Task<Unit> Send(UserProfile user, Test test, string template, bool autoCheck)
        {
            template = template
                .Replace("{user_name}", user.FirstName + " " + user.LastName)
                .Replace("{email_message}", await GetTableContent(test, autoCheck));

            var emailData = new EmailData()
            {
                subject = "Rezultatul testului",
                body = template,
                from = "Do Not Reply",
                to = user.Email
            };

            await _notificationService.Notify(emailData, NotificationType.Both);

            return Unit.Value;
        }

        private async Task<string> GetTableContent(Test test, bool autoCheck)
        {
            var content = string.Empty;

            if (autoCheck)
            {
                content = $@"<p style=""font-size: 22px; font-weight: 300;"">Testul ""{test.TestTemplate.Name}"" a fost verificat.</p>";
            }
            else
            {
                content += $@"<p style=""font-size: 22px; font-weight: 300;"">Toți candidații asignati la evenimentul ""{test.Event.Name}"" au finisat testul.</p>
                         <p style=""font-size: 22px;font-weight: 300;"">Puteți începe verificarea.</p>";
            }

            return content;
        }
    }
}
