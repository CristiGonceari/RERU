using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Evaluation.Application.TestTypes.AddTestTypeRules
{
    public class AddTestTypeRulesCommandValidator : AbstractValidator<AddTestTypeRulesCommand>
    {
        public AddTestTypeRulesCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(r => r.Data)
                .NotNull()
                .WithErrorCode(ValidationCodes.NULL_OR_EMPTY_INPUT);

            When(r => r.Data != null, () =>
            {
                RuleFor(x => x.Data.Rules)
                    .NotEmpty()
                    .WithErrorCode(ValidationCodes.EMPTY_RULES);

                RuleFor(x => x.Data.TestTypeId)
                    .SetValidator(x => new ItemMustExistValidator<TestType>(appDbContext, ValidationCodes.INVALID_TEST_TYPE,
                        ValidationMessages.InvalidReference));
            });
        }
    }
}
