using CODWER.RERU.Core.Application.Validation;
using CODWER.RERU.Core.Application.Validators.IDNP;
using CODWER.RERU.Core.Data.Persistence.Context;
using CVU.ERP.Common.DataTransferObjects.Users;
using CVU.ERP.Common.Extensions;
using CVU.ERP.Common.Validation;
using FluentValidation;
using FluentValidation.Validators;
using System.Linq;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.Users.InregistrateUser
{
    public class InregistrateUserCommandValidator : AbstractValidator<InregistrateUserCommand>
    {
        private readonly AppDbContext _appDbContext;

        public InregistrateUserCommandValidator(IValidator<CreateUserDto> createUserDto, AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

            RuleFor(x => x).NotEmpty()
                .Must(x=> CheckCode(x.Code, x.Email))
                .WithErrorCode(ValidationCodes.INVALID_CODE);

            RuleFor(x => x.FirstName).NotEmpty()
                .WithMessage(ValidationMessages.InvalidInput)
                .WithErrorCode(ValidationCodes.EMPTY_USER_NAME);

            RuleFor(x => x.LastName).NotEmpty()
                .WithMessage(ValidationMessages.InvalidInput)
                .WithErrorCode(ValidationCodes.EMPTY_USER_LAST_NAME);

            RuleFor(x => x.FatherName).NotEmpty()
                .WithMessage(ValidationMessages.InvalidInput)
                .WithErrorCode(ValidationCodes.EMPTY_USER_FATHER_NAME);

            RuleFor(x => x.Email).NotEmpty()
                .WithMessage(ValidationMessages.InvalidInput)
                .WithErrorCode(ValidationCodes.EMPTY_USER_EMAIL);

            RuleFor(x => x.Idnp)
                .SetValidator(new IdnpValidator());

            RuleFor(x => x.Idnp)
              .Custom(CheckIfUniqueIdnpOnCreate);

        }

        private bool CheckCode(string code, string email)
        {
            var emailVerification = _appDbContext.EmailVerifications.FirstOrDefault(x => x.Email == email);

            if (emailVerification != null)
            {
                if (emailVerification.Code == code)
                {
                    return true;
                }
            }

            return false;
        }

        private void CheckIfUniqueIdnpOnCreate(string idnp, CustomContext context)
        {
            var exist = _appDbContext.UserProfiles.Any(x => x.Idnp == idnp);

            if (exist)
            {
                context.AddFail(ValidationCodes.DUPLICATE_IDNP_IN_SYSTEM, ValidationMessages.InvalidReference);
            }
        }
    }
}
