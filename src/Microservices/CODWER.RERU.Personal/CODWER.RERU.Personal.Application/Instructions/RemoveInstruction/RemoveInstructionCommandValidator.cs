using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using RERU.Data.Entities.PersonalEntities.ContractorEvents;
using RERU.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Instructions.RemoveInstruction
{
    public class RemoveInstructionCommandValidator : AbstractValidator<RemoveInstructionCommand>
    {
        public RemoveInstructionCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .SetValidator(new ItemMustExistValidator<Instruction>(appDbContext, ValidationCodes.INSTRUCTION_NOT_FOUND, ValidationMessages.NotFound));
        }
    }
}
