using CODWER.RERU.Core.Application.Validation;
using CODWER.RERU.Core.DataTransferObjects.Autobiography;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.Autobiographies
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
               .SetValidator(new ItemMustExistValidator<Contractor>(appDbContext, ValidationCodes.USER_NOT_FOUND,
                   ValidationMessages.InvalidReference));
        }
    }
}
