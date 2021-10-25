using CODWER.RERU.Core.DataTransferObjects.Users;
using FluentValidation;

namespace CODWER.RERU.Core.Application.Users.EditUserPersonalDetails
{
    public class EditUserPersonalDetailsCommandValidator : AbstractValidator<EditUserPersonalDetailsCommand>
    {
        public EditUserPersonalDetailsCommandValidator(IValidator<EditUserPersonalDetailsDto> editUserDto)
        {
            RuleFor(x => x.Data)
                .SetValidator(editUserDto);
        }
    }
}
