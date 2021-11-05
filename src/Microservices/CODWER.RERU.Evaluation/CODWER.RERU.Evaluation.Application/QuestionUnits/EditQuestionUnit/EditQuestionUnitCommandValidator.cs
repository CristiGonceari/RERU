using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.QuestionUnits.EditQuestionUnit
{
    public class EditQuestionUnitCommandValidator : AbstractValidator<EditQuestionUnitCommand>
    {
        private readonly AppDbContext _appDbContext;
        public EditQuestionUnitCommandValidator(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

            When(r => r.Data != null, () =>
            {
                RuleFor(x => x.Data.Id)
                    .Must(x => appDbContext.QuestionUnits.Any(q => q.Id == x))
                    .WithErrorCode(ValidationCodes.INVALID_QUESTION);

                RuleFor(x => x.Data.QuestionCategoryId)
                .Must(x => appDbContext.QuestionCategories.Any(q => q.Id == x))
                .WithErrorCode(ValidationCodes.INVALID_CATEGORY);

                RuleFor(x => x.Data.Question)
                    .NotEmpty()
                    .WithErrorCode(ValidationCodes.EMPTY_QUESTION);

                RuleFor(x => x.Data.QuestionType)
                    .NotNull()
                    .IsInEnum()
                    .WithErrorCode(ValidationCodes.EMPTY_QUESTION_TYPE);

                RuleFor(x => x.Data.Status)
                    .IsInEnum()
                    .WithErrorCode(ValidationCodes.EMPTY_QUESTION_STATUS);

                RuleFor(x => x.Data.QuestionPoints)
                    .GreaterThan(0)
                    .WithErrorCode(ValidationCodes.INVALID_QUESTION_POINTS);

                When(x => appDbContext.TestQuestions.Any(t => t.QuestionUnitId == x.Data.Id), () =>
                {
                    RuleFor(x => x.Data.Id.Value)
                    .Must(x => !IsQuestionInActiveTest(x))
                    .WithErrorCode(ValidationCodes.QUESTION_IS_IN_ACTIVE_TEST_TYPE);
                });
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
                if (test.TestType.Status == TestTypeStatusEnum.Active)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
