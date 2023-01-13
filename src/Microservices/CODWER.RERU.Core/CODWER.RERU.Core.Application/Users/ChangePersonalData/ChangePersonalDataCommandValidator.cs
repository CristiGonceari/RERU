using System.Text.RegularExpressions;
using CODWER.RERU.Core.Application.Validation;
using CODWER.RERU.Core.DataTransferObjects.Users;
using CVU.ERP.Common;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Core.Application.Users.ChangePersonalData {
    public class ChangePersonalDataCommandValidator : AbstractValidator<ChangePersonalDataCommand> 
    {
        public ChangePersonalDataCommandValidator (IValidator<UserPersonalDataDto> userPersonalDataDto, IDateTime dateTime) 
        {
            RuleFor (x => x.User)
                .SetValidator (userPersonalDataDto);

            RuleFor(r => r.User.PhoneNumber)
                .Must(x => Regex.IsMatch(x, @"^(\+373[0-9]{8})$"))
                .WithErrorCode(ValidationCodes.INVALID_USER_PHONE);

            RuleFor(r => r.User.BirthDate)
                .Must(x => x < dateTime.Now.AddYears(-18))
                .WithErrorCode(ValidationCodes.INVALID_USER_BIRTH_DATE);

            RuleFor(x => x.User.Email).NotEmpty()
                .WithMessage(ValidationMessages.InvalidInput)
                .WithErrorCode(ValidationCodes.EMPTY_USER_EMAIL)
                .EmailAddress()
                .WithMessage(ValidationMessages.InvalidInput)
                .WithErrorCode(ValidationCodes.INVALID_EMAIL_FORMAT);
        }
    }
}
