using CODWER.RERU.Core.Application.Validation;
using CODWER.RERU.Core.DataTransferObjects.Users;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Core.Application.Users.EditUserPersonalDetails
{
    public class EditUserPersonalDetailsCommandValidator : AbstractValidator<EditUserPersonalDetailsCommand>
    {
        public EditUserPersonalDetailsCommandValidator(IValidator<EditUserPersonalDetailsDto> editUserDto)
        {
            RuleFor(x => x.Data)
                .SetValidator(editUserDto);

            RuleFor(x => x.Data.AccessModeEnum).NotEmpty()
                .WithMessage(ValidationMessages.InvalidInput)
                .WithErrorCode(ValidationCodes.EMPTY_ACCESS_MODE);
        }
    }
}
