using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Options.EditOption
{
    public class EditOptionsCommandValidator : AbstractValidator<EditOptionsCommand>
    {
        private readonly AppDbContext _appDbContext;
        public EditOptionsCommandValidator(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

            RuleFor(r => r.Data.Id)
                .Must(x => appDbContext.Options.Any(d => d.Id == x))
                .WithErrorCode(ValidationCodes.INVALID_ID);

            RuleFor(r => r.Data.QuestionUnitId)
                .Must(x => appDbContext.QuestionUnits.Any(d => d.Id == x))
                .WithErrorCode(ValidationCodes.INVALID_QUESTION);

            RuleFor(r => r.Data.Answer)
                .NotEmpty()
                .WithErrorCode(ValidationCodes.EMPTY_ANSWER);

            RuleFor(r => r.Data.IsCorrect)
                .NotNull()
                .WithErrorCode(ValidationCodes.EMPTY_CORRECT_ANSWER);

            When(x => appDbContext.TestQuestions.Any(t => t.QuestionUnitId == x.Data.QuestionUnitId), () =>
            {
                RuleFor(x => x.Data.QuestionUnitId)
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
