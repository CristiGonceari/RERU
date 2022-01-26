using System.Linq;
using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using CODWER.RERU.Personal.Data.Entities;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CVU.ERP.Common.Extensions;
using CVU.ERP.Common.Validation;
using FluentValidation;
using FluentValidation.Validators;

namespace CODWER.RERU.Personal.Application.UserProfiles.CreateUserForContractor
{
    public class CreateUserForContractorCommandValidator : AbstractValidator<CreateUserForContractorCommand>
    {
        private readonly AppDbContext _appDbContext;

        public CreateUserForContractorCommandValidator(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

            RuleFor(x => x.ContractorId)
                .SetValidator(new ItemMustExistValidator<Contractor>(appDbContext, ValidationCodes.CONTRACTOR_NOT_FOUND, ValidationMessages.InvalidReference));

            RuleFor(x=>x.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage(ValidationMessages.InvalidInput)
                .WithErrorCode(ValidationCodes.INVALID_INPUT);

            RuleFor(x => x)
                .Custom(CheckExistentUserProfileWithSameEmail);
        }

        private void CheckExistentUserProfileWithSameEmail(CreateUserForContractorCommand req, CustomContext context)
        {
            var exist = _appDbContext.UserProfiles.Any(x => x.Email == req.Email && x.ContractorId != req.ContractorId && x.ContractorId != null);

            if (exist)
            {
                context.AddFail(ValidationCodes.EXISTENT_RECORD, ValidationMessages.InvalidInput);
            }
        }
    }
}