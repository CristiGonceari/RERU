using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using CODWER.RERU.Personal.Data.Entities;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Instructions.AddInstruction
{
    public class AddInstructionCommandValidator : AbstractValidator<AddInstructionCommand>
    {
        public AddInstructionCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data.ContractorId)
                .SetValidator(new ItemMustExistValidator<Contractor>(appDbContext, ValidationCodes.CONTRACTOR_NOT_FOUND,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x.Data.Duration)
                .Must(x=>x > 0)
                .WithMessage(ValidationMessages.InvalidInput)
                .WithErrorCode(ValidationCodes.INVALID_INPUT);
        }
    }
}
