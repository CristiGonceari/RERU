
using CODWER.RERU.Core.DataTransferObjects.Users;
using FluentValidation;

namespace CODWER.RERU.Core.Application.Users.EditUser
{
    public class EditUserCommandValidator : AbstractValidator<EditUserCommand>
    {
        public EditUserCommandValidator(IValidator<EditUserDto> editUserDto)
        {
            RuleFor(x => x.User)
                .SetValidator(editUserDto);
        }
    }
}
