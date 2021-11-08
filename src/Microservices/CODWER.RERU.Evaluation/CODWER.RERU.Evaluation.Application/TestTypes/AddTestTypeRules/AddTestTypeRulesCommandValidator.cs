﻿using System.Linq;
using CODWER.RERU.Evaluation.Application.Validation;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using FluentValidation;

namespace CODWER.RERU.Evaluation.Application.TestTypes.AddTestTypeRules
{
    public class AddTestTypeRulesCommandValidator : AbstractValidator<AddTestTypeRulesCommand>
    {
        public AddTestTypeRulesCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(r => r.Input)
                .NotNull()
                .WithErrorCode(ValidationCodes.NULL_OR_EMPTY_INPUT);

            When(r => r.Input != null, () =>
            {
                RuleFor(x => x.Input.Rules)
                    .NotEmpty()
                    .WithErrorCode(ValidationCodes.EMPTY_RULES);

                RuleFor(x => x.Input.TestTypeId)
                    .Must(x => appDbContext.TestTypes.Any(t => t.Id == x))
                    .WithErrorCode(ValidationCodes.INVALID_TEST_TYPE);
            });
        }
    }
}
