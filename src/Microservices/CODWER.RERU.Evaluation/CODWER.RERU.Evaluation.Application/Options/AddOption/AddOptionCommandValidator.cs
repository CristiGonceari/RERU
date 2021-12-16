using FluentValidation;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;

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
