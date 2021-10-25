using CVU.ERP.Common.DataTransferObjects.Users;
using CODWER.RERU.Core.DataTransferObjects.Users;
using FluentValidation;

namespace CODWER.RERU.Core.Application.Users.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator(IValidator<CreateUserDto> createUserDto)
        {
            RuleFor(x => x.User)
                .SetValidator(createUserDto);
        }
    }
}
