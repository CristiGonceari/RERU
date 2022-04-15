﻿using CODWER.RERU.Evaluation.Application.Validation;
using FluentValidation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Locations.EditLocation
{
    public class EditLocationCommandValidator : AbstractValidator<EditLocationCommand>
    {
        public EditLocationCommandValidator(AppDbContext appDbContext)
        {
            When(r => r.Data != null, () =>
            {
                RuleFor(x => x.Data.Id)
                    .SetValidator(x => new ItemMustExistValidator<Location>(appDbContext, ValidationCodes.INVALID_LOCATION,
                        ValidationMessages.InvalidReference));

                RuleFor(r => r.Data.Name)
                    .NotEmpty()
                    .WithErrorCode(ValidationCodes.EMPTY_LOCATION_NAME);

                RuleFor(r => r.Data.Address)
                    .NotEmpty()
                    .WithErrorCode(ValidationCodes.EMPTY_ADDRESS);

                RuleFor(r => r.Data.Places)
                    .NotNull()
                    .GreaterThan(0)
                    .WithErrorCode(ValidationCodes.EMPTY_COMPUTERS_COUNT);
            });
        }
    }
}
