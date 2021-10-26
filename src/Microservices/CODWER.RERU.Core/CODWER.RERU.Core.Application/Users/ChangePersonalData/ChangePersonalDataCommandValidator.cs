using CODWER.RERU.Core.DataTransferObjects.Users;
using FluentValidation;

namespace CODWER.RERU.Core.Application.Users.ChangePersonalData {
    public class ChangePersonalDataCommandValidator : AbstractValidator<ChangePersonalDataCommand> 
    {
        public ChangePersonalDataCommandValidator (IValidator<UserPersonalDataDto> userPersonalDataDto) 
        {
            RuleFor (x => x.User)
                .SetValidator (userPersonalDataDto);
        }
    }
}
