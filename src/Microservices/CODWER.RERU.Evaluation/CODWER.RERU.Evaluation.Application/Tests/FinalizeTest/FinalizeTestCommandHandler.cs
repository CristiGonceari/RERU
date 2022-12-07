using System.Collections.Generic;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Application.VerificationTests.AutoCheckTestScore;
using CODWER.RERU.Evaluation.Application.VerificationTests.AutoVerificationTestQuestions;
using CVU.ERP.Notifications.Email;
using CVU.ERP.Notifications.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Tests.FinalizeTest
{
    public class FinalizeTestCommandHandler : IRequestHandler<FinalizeTestCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMediator _mediator;
        private readonly INotificationService _notificationService;
        private readonly IInternalNotificationService _internalNotificationService;

        public FinalizeTestCommandHandler(
            AppDbContext appDbContext, 
            IMediator mediator, 
            INotificationService notificationService, 
            IInternalNotificationService internalNotificationService)
        {
            _appDbContext = appDbContext;
            _mediator = mediator;
            _notificationService = notificationService;
            _internalNotificationService = internalNotificationService;
        }

        public async Task<Unit> Handle(FinalizeTestCommand request, CancellationToken cancellationToken)
        {
            var testToFinalize = await GetTest(request.TestId);

            testToFinalize.TestStatus = TestStatusEnum.Terminated;

            await _appDbContext.SaveChangesAsync();

            await AutoVerificationTest(testToFinalize);

            if (testToFinalize.TestTemplate.Mode == TestTemplateModeEnum.Test)
            {
                await SendEmailNotificationToEvaluators(testToFinalize);

                if (testToFinalize.Event != null)
                {
                    await SendEmailPositionResponsiblePerson(testToFinalize);
                }
            }

            await FinalizeAllTestsWithTheSameHash(testToFinalize);

            return Unit.Value;
        }

        private async Task<Test> GetTest(int testId) => await _appDbContext.Tests
            .Include(x => x.TestTemplate)
            .Include(x => x.Event)
            .ThenInclude(x => x.EventUsers)
            .ThenInclude(x => x.CandidatePosition)
            .Include(x => x.Event)
            .ThenInclude(x => x.EventVacantPositions)
            .ThenInclude(x => x.CandidatePosition)
            .FirstAsync(x => x.Id == testId);

        private async Task AutoVerificationTest(Test test)
        {
            await _mediator.Send(new AutoVerificationTestQuestionsCommand { TestId = test.Id });

            if (test.TestQuestions.All(x => x.QuestionUnit.QuestionType == QuestionTypeEnum.OneAnswer || x.QuestionUnit.QuestionType == QuestionTypeEnum.MultipleAnswers))
            {
                await _mediator.Send(new AutoCheckTestScoreCommand { TestId = test.Id });
                test.TestStatus = TestStatusEnum.Verified;
                await _appDbContext.SaveChangesAsync();

                await SendEmailNotificationToCandidate(test);
            }
        }

        private async Task FinalizeAllTestsWithTheSameHash(Test testToFinalize)
        {
            var testsWithTheSameHash = _appDbContext.Tests
                .Where(x => x.HashGroupKey == testToFinalize.HashGroupKey);

            if (testsWithTheSameHash.Any())
            {
                foreach (var test in testsWithTheSameHash.ToList())
                {
                    test.TestStatus = TestStatusEnum.Terminated;

                    await _appDbContext.SaveChangesAsync();

                    await AutoVerificationTest(test);
                }
            }

            await _appDbContext.SaveChangesAsync();
        }

        #region CandidatePositionMail
        private async Task SendEmailPositionResponsiblePerson(Test test)
        {
            var eventUser = test.Event.EventUsers
                .FirstOrDefault(x => x.UserProfileId == test.UserProfileId);

            if (eventUser != null)
            {
                var position = _appDbContext.CandidatePositions.FirstOrDefault(x => x.Id == eventUser.PositionId);

                if (position != null)
                {
                    var candidatePositionNotifications = _appDbContext.CandidatePositionNotifications
                        .Where(x => x.CandidatePositionId == position.Id)
                        .ToList();

                    foreach (var item in candidatePositionNotifications)
                    {
                        var userProfile = _appDbContext.UserProfiles.FirstOrDefault(x => x.Id == item.UserProfileId);

                        await Send(userProfile, "Pozitia candidata", await GetCandidatePositionMessage(test, test.UserProfile.FullName, position?.Name));
                    }
                }
            }
        }

        private async Task<string> GetCandidatePositionMessage(Test finalizeTest, string candidateUserProfileName, string positionName) =>
            $@"<p style=""font-size: 22px; font-weight: 300;"">candidatul ""{candidateUserProfileName}"", </p>
                            <p style=""font-size: 22px;font-weight: 300;"">care a candidat la pozitia ""{positionName}"", 
                            a finalizat testul ""{finalizeTest.TestTemplate.Name}"", din cadrul evenimentului ""{finalizeTest.Event.Name}"".</p>";
        #endregion

        #region EvaluatorMail
        private async Task<Unit> SendEmailNotificationToEvaluators(Test testToFinalize)
        {
            var eventEvaluators = await _appDbContext.EventEvaluators
                .Include(x => x.Evaluator)
                .Include(x => x.Event)
                .Where(x => x.EventId == testToFinalize.EventId)
                .ToListAsync();

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
                    await Send(evaluator.Evaluator, "Rezultatul testului", await GetEvaluatorMessage(testToFinalize));

                    await _internalNotificationService.AddNotification(evaluator.EvaluatorId, NotificationMessages.AllCandidatesFinishedTest);
                }
                else
                {
                    return Unit.Value;
                }
            }

            return Unit.Value;
        }

        private async Task<string> GetEvaluatorMessage(Test test) => $@"<p style=""font-size: 22px; font-weight: 300;"">toți candidații asignati la evenimentul ""{test.Event.Name}"" au finisat testul.</p>
                         <p style=""font-size: 22px;font-weight: 300;"">Puteți începe verificarea.</p>";
        #endregion

        #region CandidateMail
        private async Task<Unit> SendEmailNotificationToCandidate(Test testToFinalize)
        {
            var candidate = await _appDbContext.UserProfiles.FirstOrDefaultAsync(x => x.Id == testToFinalize.UserProfileId);

            await _internalNotificationService.AddNotification(candidate.Id, NotificationMessages.YourTestWasVerified);

            await Send(candidate, "Rezultatul testului", await GetCandidateMessage(testToFinalize));

            return Unit.Value;
        }

        private async Task<string> GetCandidateMessage(Test test) => $@"<p style=""font-size: 22px; font-weight: 300;"">testul ""{test.TestTemplate.Name}"" a fost verificat.</p>
                            <p style=""font-size: 22px;font-weight: 300;"">Ați acumulat {test.AccumulatedPercentage}% din 100 %.</p>";
        #endregion

        private async Task Send(UserProfile user, string subject, string message)
        {
            await _notificationService.PutEmailInQueue(new QueuedEmailData
            {
                Subject = subject,
                To = user.Email,
                HtmlTemplateAddress = "Templates/Evaluation/EmailNotificationTemplate.html",
                ReplacedValues = new Dictionary<string, string>()
                {
                    { "{user_name}", user.FullName },
                    { "{email_message}", message }
                }
            });
        }
    }
}
