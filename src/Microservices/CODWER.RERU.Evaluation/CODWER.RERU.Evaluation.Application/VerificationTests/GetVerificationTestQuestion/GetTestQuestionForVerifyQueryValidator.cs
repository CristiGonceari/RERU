using System.Linq;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.VerificationTests;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.VerificationTests.GetVerificationTestQuestion
{
    public class GetTestQuestionForVerifyQueryValidator : AbstractValidator<GetTestQuestionForVerifyQuery>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IUserProfileService _userProfileService;
        public GetTestQuestionForVerifyQueryValidator(AppDbContext appDbContext, IUserProfileService userProfileService)
        {
            _appDbContext = appDbContext;
            _userProfileService = userProfileService;

            RuleFor(x => x.Data)
                .Must(x => appDbContext.TestQuestions
                    .Any(t => t.Id == appDbContext.Tests.FirstOrDefault(ts => ts.Id == x.TestId).TestQuestions.FirstOrDefault(q => q.Index == x.QuestionIndex).Id))
                .WithErrorCode(ValidationCodes.INVALID_TEST_QUESTION);

            When(r => r.Data.ToEvaluate == true, () =>
            {
                RuleFor(x => x.Data)
                    .MustAsync((x, cancellation) => IsEvaluator(x))
                    .WithErrorCode(ValidationCodes.INVALID_EVALUATOR_FOR_THIS_TEST);
            });

            When(r => r.Data.ToEvaluate == false, () =>
            {
                RuleFor(x => x.Data)
                    .MustAsync((x, cancellation) => IsCandidate(x))
                    .WithErrorCode(ValidationCodes.CANT_VIEW_TEST_RESULT);
            });
        }

        private async Task<bool> IsEvaluator(VerificationTestQuestionDto data)
        {
            var currentUser = await _userProfileService.GetCurrentUser();
            var isEvaluator = false;

            var test = _appDbContext.Tests
                .Include(t => t.TestTemplate)
                    .ThenInclude(tt => tt.Settings)
                .Include(t => t.TestQuestions)
                    .ThenInclude(tq => tq.QuestionUnit)
                .FirstOrDefault(t => t.Id == data.TestId);

            var eventEvaluators = _appDbContext.EventEvaluators
                .Include(ee => ee.Event)
                .Where(x => x.EventId == test.EventId);

            if (test != null && test.EvaluatorId != null)
            {
                isEvaluator = _appDbContext.Tests.Any(t => t.Id == test.Id && t.EvaluatorId == currentUser.Id);
            } 
            else if (test != null && test.EventId != null && eventEvaluators.Count() > 0)
            {
                isEvaluator = _appDbContext.EventEvaluators.Any(e => e.EventId == test.EventId && e.EvaluatorId == currentUser.Id);
            }

            return isEvaluator;
        }

        private async Task<bool> IsCandidate(VerificationTestQuestionDto data)
        {
            var currentUser = await _userProfileService.GetCurrentUser();
            var isCandidate = false;

            var test = _appDbContext.Tests
                .Include(t => t.TestTemplate)
                    .ThenInclude(tt => tt.Settings)
                .Include(t => t.TestQuestions)
                    .ThenInclude(tq => tq.QuestionUnit)
                .FirstOrDefault(t => t.Id == data.TestId);

            if (test != null && test.TestTemplate.Settings.CanViewResultWithoutVerification)
            {
                isCandidate = _appDbContext.Tests.Any(t => t.Id == test.Id && t.UserProfileId == currentUser.Id);
            }

            var isEvaluator = await IsEvaluator(data);

            return isCandidate || isEvaluator;
        }
    }
}
