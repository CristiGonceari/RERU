using System;
using System.Text.RegularExpressions;
using CODWER.RERU.Core.Application.Validation;
using CODWER.RERU.Core.DataTransferObjects.Users;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.Users.EditUserPersonalDetails
{
    public class EditUserPersonalDetailsCommandValidator : AbstractValidator<EditUserPersonalDetailsCommand>
    {
        public EditUserPersonalDetailsCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(r => r.Data)
                .NotNull()
                .WithErrorCode(ValidationCodes.NULL_OR_EMPTY_INPUT);

            When(r => r.Data != null, () =>
            {
                RuleFor(x => x.Data.Id)
                    .SetValidator(x => new ItemMustExistValidator<UserProfile>(appDbContext, ValidationCodes.INVALID_ID,
                        ValidationMessages.InvalidReference));

                RuleFor(r => r.Data.FirstName)
                    .NotEmpty()
                    .WithErrorCode(ValidationCodes.EMPTY_USER_LAST_NAME);

                RuleFor(r => r.Data.LastName)
                    .NotEmpty()
                    .WithErrorCode(ValidationCodes.EMPTY_USER_LAST_NAME);

                RuleFor(r => r.Data.FatherName)
                    .NotEmpty()
                    .WithErrorCode(ValidationCodes.EMPTY_USER_LAST_NAME);

                RuleFor(r => r.Data.PhoneNumber)
                    .Must(x => Regex.IsMatch(x, @"^(\+373[0-9]{8})$"))
                    .WithErrorCode(ValidationCodes.INVALID_USER_PHONE);

                RuleFor(r => r.Data.BirthDate)
                    .Must(x => x < DateTime.Now.AddYears(-18))
                    .WithErrorCode(ValidationCodes.INVALID_USER_BIRTH_DATE);

                RuleFor(x => x.Data.AccessModeEnum)
                    .NotNull()
                    .WithErrorCode(ValidationCodes.EMPTY_ACCESS_MODE);
            });
        }
    }
}
