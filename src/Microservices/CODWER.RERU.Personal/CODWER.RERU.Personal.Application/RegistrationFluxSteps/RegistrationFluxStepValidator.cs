using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using CODWER.RERU.Personal.Application.Validators.EnumValidators;
using CODWER.RERU.Personal.DataTransferObjects.RegistrationFluxSteps;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities.Enums;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Personal.Application.RegistrationFluxSteps
{
    public class RegistrationFluxStepValidator : AbstractValidator<RegistrationFluxStepDto>
    {
        public RegistrationFluxStepValidator(AppDbContext appDbContext)
        {
            RuleFor(x => (int)x.Step)
                .SetValidator(new ExistInEnumValidator<RegistrationFluxStepEnum>());

            RuleFor(x => x.IsDone)
                .NotEmpty()
                .WithErrorCode(ValidationCodes.INVALID_INPUT)
                .WithMessage(ValidationMessages.InvalidInput);

            RuleFor(x => x.ContractorId)
                .SetValidator(new ItemMustExistValidator<Contractor>(appDbContext, ValidationCodes.CONTRACTOR_NOT_FOUND, ValidationMessages.NotFound));
        }
    }
}
