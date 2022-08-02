using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Personal.Application.Autobiographies.UpdateAutobiography
{
    public class UpdateAutobiographyCommandValidator : AbstractValidator<UpdateAutobiographyCommand>
    {
        public UpdateAutobiographyCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data.Id)
               .SetValidator(new ItemMustExistValidator<Autobiography>(appDbContext, ValidationCodes.ADDRESS_NOT_FOUND,
                   ValidationMessages.InvalidReference));

            RuleFor(x => x.Data)
                .SetValidator(new AutobiographyValidator(appDbContext));
        }
    }
}
