﻿using CODWER.RERU.Evaluation.Application.Validation;
using FluentValidation;
using System;

namespace CODWER.RERU.Evaluation.Application.Plans.AddPlan
{
    public class AddPlanCommandValidator : AbstractValidator<AddPlanCommand>
    {
        public AddPlanCommandValidator()
        {
            RuleFor(r => r.Data)
                .NotNull()
                .WithErrorCode(ValidationCodes.NULL_OR_EMPTY_INPUT);

            When(r => r.Data != null, () =>
            {
                RuleFor(r => r.Data.Name)
                    .NotEmpty()
                    .WithErrorCode(ValidationCodes.EMPTY_NAME);

                RuleFor(r => r.Data.FromDate)
                    .GreaterThan(new DateTime(2000, 1, 1))
                    .WithErrorCode(ValidationCodes.INVALID_START_DATE);

                RuleFor(r => r.Data.TillDate)
                    .GreaterThan(new DateTime(2000, 1, 1))
                    .WithErrorCode(ValidationCodes.INVALID_END_DATE);

                RuleFor(r => r.Data)
                    .Must(x => x.TillDate > x.FromDate)
                    .WithErrorCode(ValidationCodes.INVALID_TIME_RANGE);

                RuleFor(r => r.Data)
                    .Must(x => x.FromDate >= DateTime.Today)
                    .WithErrorCode(ValidationCodes.START_DAY_CANT_BE_FROM_PAST);
            });
        }
    }

}
