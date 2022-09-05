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
        private readonly ICandidatePositionService _candidatePositionService;

        public FinalizeTestCommandHandler(
            AppDbContext appDbContext, 
            IMediator mediator, 
            INotificationService notificationService, 
            IInternalNotificationService internalNotificationService, 
            ICandidatePositionService candidatePositionService)
        {
            _appDbContext = appDbContext;
            _mediator = mediator;
            _notificationService = notificationService;
            _internalNotificationService = internalNotificationService;
            _candidatePositionService = candidatePositionService;
        }

        public async Task<Unit> Handle(FinalizeTestCommand request, CancellationToken cancellationToken)
        {
            var testToFinalize = await _appDbContext.Tests
                .Include(x => x.TestTemplate)
                .Include(x => x.Event)
                    .ThenInclude(x => x.EventUsers)
                        .ThenInclude(x => x.CandidatePosition)
                .Include(x => x.Event)
                    .ThenInclude(x => x.EventVacantPositions)
                        .ThenInclude(x => x.CandidatePosition)
                .FirstAsync(x => x.Id == request.TestId);

            testToFinalize.TestStatus = TestStatusEnum.Terminated;
            var autocheck = false;

            await _appDbContext.SaveChangesAsync();

            await _mediator.Send(new AutoVerificationTestQuestionsCommand { TestId = request.TestId });

            if (testToFinalize.TestQuestions.All(x => x.QuestionUnit.QuestionType == QuestionTypeEnum.OneAnswer || x.QuestionUnit.QuestionType == QuestionTypeEnum.MultipleAnswers))
            {
                await _mediator.Send(new AutoCheckTestScoreCommand { TestId = request.TestId });
                testToFinalize.TestStatus = TestStatusEnum.Verified;
                await _appDbContext.SaveChangesAsync();

                autocheck = true;

            }

            if (testToFinalize.TestTemplate.Mode == TestTemplateModeEnum.Test)
            {
                await SendEmailNotification(testToFinalize, autocheck);
                await EmailPositionResponsiblePerson(testToFinalize);
            }

            return Unit.Value;
        }

        private async Task<Unit> SendEmailNotification(Test testToFinalize, bool autoCheck)
        {
            var eventEvaluators = await _appDbContext.EventEvaluators
                    .Include(x => x.Evaluator)
                    .Include(x => x.Event)
                    .Where(x => x.EventId == testToFinalize.EventId)
                    .ToListAsync();

            var candidate = await _appDbContext.UserProfiles.FirstOrDefaultAsync(x => x.Id == testToFinalize.UserProfileId);

            if (autoCheck)
            {
                await Send(candidate, testToFinalize, autoCheck);

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
                        await Send(evaluator.Evaluator, testToFinalize, autoCheck);

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

        private async Task EmailPositionResponsiblePerson(Test test)
        {
            var eventUser = test.Event.EventUsers
                .FirstOrDefault(x => x.UserProfileId == test.UserProfileId);

            var position = _appDbContext.CandidatePositions.FirstOrDefault(x => x.Id == eventUser.PositionId);

            var responsiblePerson = _candidatePositionService.GetResponsiblePerson(int.Parse(position?.CreateById ?? "0"));

            await SendEmailForCandidatePosition(responsiblePerson, test, position?.Name);
        }

        private async Task<Unit> Send(UserProfile user, Test test, bool autoCheck)
        {
            await _notificationService.PutEmailInQueue(new QueuedEmailData
            {
                Subject = "Rezultatul testului",
                To = user.Email,
                HtmlTemplateAddress = "Templates/Evaluation/EmailNotificationTemplate.html",
                ReplacedValues = new Dictionary<string, string>()
                {
                    { "{user_name}", user.FullName },
                    { "{email_message}", await GetTableContent(test, autoCheck) }
                }
            });

            return Unit.Value;
        }

        private async Task<Unit> SendEmailForCandidatePosition(UserProfile user, Test test, string positionName)
        {
            await _notificationService.PutEmailInQueue(new QueuedEmailData
            {
                Subject = "Pozitia candidata",
                To = user.Email,
                HtmlTemplateAddress = "Templates/Evaluation/EmailNotificationTemplate.html",
                ReplacedValues = new Dictionary<string, string>()
                {
                    { "{user_name}", user.FullName },
                    { "{email_message}", await GetEmailContentForCandidatePosition(test, test.UserProfile.FullName, positionName) }
                }
            });

            return Unit.Value;
        }

        private async Task<string> GetTableContent(Test test, bool autoCheck)
        {
            var content = string.Empty;

            if (autoCheck)
            {
                content = $@"<p style=""font-size: 22px; font-weight: 300;"">Testul ""{test.TestTemplate.Name}"" a fost verificat.</p>
                            <p style=""font-size: 22px;font-weight: 300;"">Ați acumulat {test.AccumulatedPercentage}% din 100 %.</p>
                            <p style=""font-size: 22px;font-weight: 300;"">Testul a fost trecut {EnumMessages.EnumMessages.GetTestResultStatus(test.ResultStatus)}.</p> ";
            }
            else
            {
                content += $@"<p style=""font-size: 22px; font-weight: 300;"">Toți candidații asignati la evenimentul ""{test.Event.Name}"" au finisat testul.</p>
                         <p style=""font-size: 22px;font-weight: 300;"">Puteți începe verificarea.</p>";
            }

            return content;
        }

        private async Task<string> GetEmailContentForCandidatePosition(Test finalizeTest, string userProfileName, string positionName) => 
                         $@"<p style=""font-size: 22px; font-weight: 300;"">Candidatul ""{userProfileName}"" a finalizat testul</p>
                            <p style=""font-size: 22px;font-weight: 300;""> La testul ""{finalizeTest.TestTemplate.Name}"" din evenimentul ""{finalizeTest.Event.Name}"".</p>
                            <p style=""font-size: 22px;font-weight: 300;"">Care a candidat la pozitia ""{positionName}"".</p>";
    }
}
