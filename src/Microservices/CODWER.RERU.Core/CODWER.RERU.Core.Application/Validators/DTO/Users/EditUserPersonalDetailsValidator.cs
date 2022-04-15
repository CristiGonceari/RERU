using CODWER.RERU.Core.DataTransferObjects.Users;
using FluentValidation;

namespace CODWER.RERU.Core.Application.Validators.DTO.Users
{
    public class EditUserPersonalDetailsValidator : AbstractValidator<EditUserPersonalDetailsDto>
    {
        public EditUserPersonalDetailsValidator()
        {
            RuleFor(x => x.FirstName).NameRule();
            RuleFor(x => x.LastName).NameRule();
        }
    }
}