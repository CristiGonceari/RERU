using FluentValidation;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.RegistrationFluxSteps.AddRegistrationFluxStep
{
    public class AddRegistrationFluxStepCommandValidator : AbstractValidator<AddRegistrationFluxStepCommand>
    {
        public AddRegistrationFluxStepCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data).SetValidator(new RegistrationFluxStepValidator(appDbContext));

        }
    }
}
