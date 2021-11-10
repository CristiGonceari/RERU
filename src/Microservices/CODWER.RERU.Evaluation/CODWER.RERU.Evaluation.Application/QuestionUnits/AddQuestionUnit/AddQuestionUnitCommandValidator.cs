using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Evaluation.Application.QuestionUnits.AddQuestionUnit
{
    public class AddQuestionUnitCommandValidator : AbstractValidator<AddQuestionUnitCommand>
    {
        public AddQuestionUnitCommandValidator(AppDbContext appDbContext)
        {
            When(r => r.Data != null, () =>
            {
                RuleFor(x => x.Data.QuestionCategoryId)
                    .SetValidator(x => new ItemMustExistValidator<QuestionCategory>(appDbContext, ValidationCodes.INVALID_CATEGORY,
                        ValidationMessages.InvalidReference));

                RuleFor(r => r.Data.Question)
                    .NotEmpty()
                    .WithErrorCode(ValidationCodes.EMPTY_QUESTION);

                RuleFor(r => r.Data.QuestionType)
                    .IsInEnum()
                    .NotEmpty()
                    .WithErrorCode(ValidationCodes.EMPTY_QUESTION_TYPE);

                RuleFor(r => r.Data.Status)
                    .IsInEnum()
                    .WithErrorCode(ValidationCodes.EMPTY_QUESTION_STATUS);

                RuleFor(x => x.Data.QuestionPoints)
                    .GreaterThan(0)
                    .WithErrorCode(ValidationCodes.INVALID_QUESTION_POINTS);
            });

        }
    }
}
