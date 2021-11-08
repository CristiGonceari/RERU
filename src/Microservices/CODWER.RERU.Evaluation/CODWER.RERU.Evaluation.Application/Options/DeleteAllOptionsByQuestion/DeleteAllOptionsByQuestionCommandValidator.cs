using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CODWER.RERU.Evaluation.Application.Options.DeleteAllOptionsByQuestion
{

    public class DeleteAllOptionsByQuestionCommandValidator : AbstractValidator<DeleteAllOptionsByQuestionCommand>
    {
        private readonly AppDbContext _appDbContext;
        public DeleteAllOptionsByQuestionCommandValidator(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

            RuleFor(r => r.QuestionUnitId)
                .Must(x => appDbContext.QuestionUnits.Any(d => d.Id == x))
                .WithErrorCode(ValidationCodes.INVALID_QUESTION);

            When(x => appDbContext.TestQuestions.Any(t => t.QuestionUnitId == x.QuestionUnitId), () =>
            {
                RuleFor(x => x.QuestionUnitId)
                .Must(x => IsQuestionInActiveTest(x) == false)
                .WithErrorCode(ValidationCodes.QUESTION_IS_IN_ACTIVE_TEST_TYPE);
            });
        }

        private bool IsQuestionInActiveTest(int questionUnitId)
        {
            var tests = _appDbContext.Tests
                .Include(x => x.TestQuestions)
                .Include(x => x.TestType)
                .Where(t => t.TestQuestions.Any(q => q.QuestionUnitId == questionUnitId))
                .ToList();

            foreach (var test in tests)
            {
                if (test.TestType.Status == (int)TestTypeStatusEnum.Active)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
