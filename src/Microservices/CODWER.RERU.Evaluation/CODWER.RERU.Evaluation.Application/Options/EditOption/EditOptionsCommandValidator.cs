using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Options.EditOption
{
    public class EditOptionsCommandValidator : AbstractValidator<EditOptionsCommand>
    {
        private readonly AppDbContext _appDbContext;
        public EditOptionsCommandValidator(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

            RuleFor(x => x.Data.Id)
                .SetValidator(x => new ItemMustExistValidator<Option>(appDbContext, ValidationCodes.INVALID_ID,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x.Data.QuestionUnitId)
                .SetValidator(x => new ItemMustExistValidator<QuestionUnit>(appDbContext, ValidationCodes.INVALID_QUESTION,
                    ValidationMessages.InvalidReference));

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
                .WithErrorCode(ValidationCodes.QUESTION_IS_IN_ACTIVE_TEST_TEMPLATE);
            });
        }

        private bool IsQuestionInActiveTest(int questionUnitId)
        {
            var tests = _appDbContext.Tests
                .Include(x => x.TestQuestions)
                .Include(x => x.TestTemplate)
                .Where(t => t.TestQuestions.Any(q => q.QuestionUnitId == questionUnitId))
                .ToList();

            foreach (var test in tests)
            {
                if (test.TestTemplate.Status == (int)TestTemplateStatusEnum.Active)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
