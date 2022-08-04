using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Personal.Application.RegistrationFluxSteps.UpdateRegistrationFluxStep
{
    public class UpdateRegistrationFluxStepCommandValidator : AbstractValidator<UpdateRegistrationFluxStepCommand>
    {
        public UpdateRegistrationFluxStepCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data.Id)
                .SetValidator(new ItemMustExistValidator<RegistrationFluxStep>(appDbContext, ValidationCodes.REGISTRATION_FLUX_NOT_FOUND, ValidationMessages.NotFound));

            RuleFor(x => x.Data)
                .SetValidator(new RegistrationFluxStepValidator(appDbContext));
        }
    }
}
