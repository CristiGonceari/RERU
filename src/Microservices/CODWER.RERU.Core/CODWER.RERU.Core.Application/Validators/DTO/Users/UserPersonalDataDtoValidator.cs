using CODWER.RERU.Core.DataTransferObjects.Users;
using FluentValidation;

namespace CODWER.RERU.Core.Application.Validators.DTO.Users 
{
    public class UserPersonalDataDtoValidator : AbstractValidator<UserPersonalDataDto> 
    {
        public UserPersonalDataDtoValidator () 
        {
            RuleFor (x => x.Name).NameRule ();
            RuleFor (x => x.LastName).NameRule ();
        }
    }
}