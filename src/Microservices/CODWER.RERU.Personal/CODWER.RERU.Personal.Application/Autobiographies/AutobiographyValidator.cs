using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using CODWER.RERU.Personal.DataTransferObjects.Autobiography;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Personal.Application.Autobiographies
{
    public class AutobiographyValidator : AbstractValidator<AutobiographyDto>
    {
        public AutobiographyValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Text)
                .NotEmpty()
                .WithErrorCode(ValidationCodes.EMPTY_AUTOBIOGRAPHY_TEXT)
                .WithMessage(ValidationMessages.InvalidInput);

            RuleFor(x => x.ContractorId)
               .SetValidator(new ItemMustExistValidator<Contractor>(appDbContext, ValidationCodes.CONTRACTOR_NOT_FOUND,
                   ValidationMessages.InvalidReference));
        }
    }
}
