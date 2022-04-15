using FluentValidation;
using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Options.AddOption
{
    public class AddOptionCommandValidator : AbstractValidator<AddOptionCommand>
    {
        public AddOptionCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.QuestionUnitId)
                .SetValidator(x => new ItemMustExistValidator<QuestionUnit>(appDbContext, ValidationCodes.INVALID_QUESTION,
                    ValidationMessages.InvalidReference));

            RuleFor(r => r.Answer)
                .NotEmpty()
                .WithErrorCode(ValidationCodes.EMPTY_ANSWER);

            RuleFor(r => r.IsCorrect)
                .NotNull()
                .WithErrorCode(ValidationCodes.EMPTY_CORRECT_ANSWER);
        }
    }
}
