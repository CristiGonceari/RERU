using CODWER.RERU.Core.Application.Validation;
using CODWER.RERU.Core.Application.Validators.EnumValidators;
using CODWER.RERU.Core.DataTransferObjects.RegistrationFluxSteps;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.RegistrationFluxSteps
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
                .SetValidator(new ItemMustExistValidator<Contractor>(appDbContext, ValidationCodes.USER_NOT_FOUND, ValidationMessages.NotFound));
        }
    }
}
