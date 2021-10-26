using CVU.ERP.Common.DataTransferObjects.Users;
using FluentValidation;

namespace CODWER.RERU.Core.Application.Validators.DTO.Users
{
    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator()
        {
            RuleFor(x => x.Name).NameRule();
            RuleFor(x => x.LastName).NameRule();
        }
    }
}