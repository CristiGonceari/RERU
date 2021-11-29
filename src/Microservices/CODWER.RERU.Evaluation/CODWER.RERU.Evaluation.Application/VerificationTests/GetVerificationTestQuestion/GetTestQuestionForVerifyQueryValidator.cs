using System.Linq;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
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

            RuleFor(x => x.Data)
                .MustAsync((x, cancellation) => IsEvaluator(x))
                .WithErrorCode(ValidationCodes.INVALID_EVALUATOR_FOR_THIS_TEST);
        }

        private async Task<bool> IsEvaluator(VerificationTestQuestionDto input)
        {
            var test = _appDbContext.Tests
                .Include(t => t.TestType)
                .FirstOrDefault(t => t.Id == input.TestId);

            var isOnlyOneAnswerTest = _appDbContext.TestTypeQuestionCategories
                .Where(tt => tt.TestTypeId == test.TestTypeId)
                .All(tt => tt.QuestionType == QuestionTypeEnum.OneAnswer);

            var currentUser = await _userProfileService.GetCurrentUser();

            var isEvaluator = false;

            if (!isOnlyOneAnswerTest)
            {
                if (test != null && test.EventId != null && test.EvaluatorId == null)
                {
                    isEvaluator = _appDbContext.EventEvaluators.Any(e =>
                        e.EventId == test.EventId && e.EvaluatorId == currentUser.Id);
                }
                else if (test != null)
                {
                    isEvaluator = _appDbContext.Tests.Any(t => t.Id == test.Id && t.EvaluatorId == currentUser.Id);
                }
            }
            else return true;

            return isEvaluator;
        }
    }
}
