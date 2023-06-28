using System.Linq;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.DataTransferObjects.VerificationTests;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;

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

            var currentUserId = _userProfileService.GetCurrentUserId().Result;

            RuleFor(x => x.Data)
                .Must(x => appDbContext.TestQuestions
                    .Any(t => t.Id == appDbContext.Tests.FirstOrDefault(ts => ts.Id == x.TestId).TestQuestions.FirstOrDefault(q => q.Index == x.QuestionIndex).Id))
                .WithErrorCode(ValidationCodes.INVALID_TEST_QUESTION);

            When(r => r.Data.ToEvaluate == true, () =>
            {
                RuleFor(x => x.Data)
                    .MustAsync((x, cancellation) => IsEvaluator(x,currentUserId))
                    .WithErrorCode(ValidationCodes.INVALID_EVALUATOR_FOR_THIS_TEST);
            });

            When(r => r.Data.ToEvaluate == false, () =>
            {
                RuleFor(x => x.Data)
                    .MustAsync((x, cancellation) => IsCandidate(x, currentUserId))
                    .WithErrorCode(ValidationCodes.CANT_VIEW_TEST_RESULT);
            });
        }

        private async Task<bool> IsEvaluator(VerificationTestQuestionDto data, int currentUserId)
        {
            var isEvaluator = false;

            var test = _appDbContext.Tests.FirstOrDefault(t => t.Id == data.TestId);

            var eventEvaluators = _appDbContext.EventEvaluators.Where(x => x.EventId == test.EventId);

            if (test != null)
            {
                isEvaluator = test.EvaluatorId == currentUserId;

                if (test.EventId != null && eventEvaluators.Any())
                {
                    isEvaluator = eventEvaluators.Any(e => e.EvaluatorId == currentUserId);
                }
            }

            return isEvaluator;
        }

        private async Task<bool> IsCandidate(VerificationTestQuestionDto data, int currentUserId)
        {
            var isCandidate = false;

            var test = _appDbContext.Tests
                .Include(t => t.TestTemplate)
                    .ThenInclude(tt => tt.Settings)
                .Include(t => t.TestQuestions)
                    .ThenInclude(tq => tq.QuestionUnit)
                .FirstOrDefault(t => t.Id == data.TestId);

            if (test != null && test.TestTemplate.Settings.CanViewResultWithoutVerification)
            {
                isCandidate = _appDbContext.Tests.Any(t => t.Id == test.Id && t.UserProfileId == currentUserId);
            }

            var isEvaluator = await IsEvaluator(data, currentUserId);

            return isCandidate || isEvaluator;
        }
    }
}
