using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.QuestionUnits.AddQuestionUnit
{
    public class AddQuestionUnitCommandValidator : AbstractValidator<AddQuestionUnitCommand>
    {
        public AddQuestionUnitCommandValidator(AppDbContext appDbContext)
        {
            When(r => r != null, () =>
            {
                RuleFor(x => x.QuestionCategoryId)
                    .SetValidator(x => new ItemMustExistValidator<QuestionCategory>(appDbContext, ValidationCodes.INVALID_CATEGORY,
                        ValidationMessages.InvalidReference));

                RuleFor(r => r.Question)
                    .NotEmpty()
                    .WithErrorCode(ValidationCodes.EMPTY_QUESTION);

                RuleFor(r => r.QuestionType)
                    .IsInEnum()
                    .NotEmpty()
                    .WithErrorCode(ValidationCodes.EMPTY_QUESTION_TYPE);

                RuleFor(r => r.Status)
                    .IsInEnum()
                    .WithErrorCode(ValidationCodes.EMPTY_QUESTION_STATUS);

                RuleFor(x => x.QuestionPoints)
                    .GreaterThan(0)
                    .WithErrorCode(ValidationCodes.INVALID_QUESTION_POINTS);
            });

        }
    }
}
