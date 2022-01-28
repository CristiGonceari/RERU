using CODWER.RERU.Core.Application.Validation;
using CODWER.RERU.Core.Application.Validators.IDNP;
using CODWER.RERU.Core.Data.Persistence.Context;
using CVU.ERP.Common.DataTransferObjects.Users;
using CVU.ERP.Common.Extensions;
using CVU.ERP.Common.Validation;
using FluentValidation;
using FluentValidation.Validators;
using System.Linq;

namespace CODWER.RERU.Core.Application.Users.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        private readonly CoreDbContext _coreDbContext;

        public CreateUserCommandValidator(IValidator<CreateUserDto> createUserDto, CoreDbContext coreDbContext)
        {
            _coreDbContext = coreDbContext;

            RuleFor(x => x.User)
                .SetValidator(createUserDto);

            RuleFor(x => x.User.Idnp)
                .SetValidator(new IdnpValidator());

            RuleFor(x => x.User.Idnp)
              .Custom(CheckIfUniqueIdnpOnCreate);

        }
        private void CheckIfUniqueIdnpOnCreate(string idnp, CustomContext context)
        {
            var exist = _coreDbContext.UserProfiles.Any(x => x.Idnp == idnp);

            if (exist)
            {
                context.AddFail(ValidationCodes.DUPLICATE_IDNP_IN_SYSTEM, ValidationMessages.InvalidReference);
            }
        }
    }
}
