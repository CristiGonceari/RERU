using System.Linq;
using CODWER.RERU.Core.Application.Validation;
using CVU.ERP.Common.Extensions;
using CVU.ERP.Common.Validation;
using FluentValidation;
using FluentValidation.Validators;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.Users.EditUserFromColaborator
{
    public class EditUserFromColaboratorCommandValidator : AbstractValidator<EditUserFromColaboratorCommand>
    {
        private readonly AppDbContext _appDbContext;
        public EditUserFromColaboratorCommandValidator(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

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
                .WithErrorCode(ValidationCodes.EMPTY_USER_EMAIL)
                .EmailAddress()
                .WithMessage(ValidationMessages.InvalidInput)
                .WithErrorCode(ValidationCodes.INVALID_EMAIL_FORMAT);

            RuleFor(x => x)
              .Custom(CheckIfUniqueIdnpOnCreate);
        }

        private void CheckIfUniqueIdnpOnCreate(EditUserFromColaboratorCommand data, CustomContext context)
        {
            var exist = _appDbContext.UserProfiles.Any(x =>x.Id != data.Id && x.Idnp == data.Idnp);

            if (exist)
            {
                context.AddFail(ValidationCodes.DUPLICATE_IDNP_IN_SYSTEM, ValidationMessages.InvalidReference);
            }
        }
    }
}
