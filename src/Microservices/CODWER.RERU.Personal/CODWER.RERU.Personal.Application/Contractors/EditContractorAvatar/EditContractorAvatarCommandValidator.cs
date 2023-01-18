using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;
using CVU.ERP.Module.Common.MessageCodes;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Contractors.EditContractorAvatar
{
    public class EditContractorAvatarCommandValidator : AbstractValidator<EditContractorAvatarCommand>
    {
        public EditContractorAvatarCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data.ContractorId)
                .SetValidator(new ItemMustExistValidator<Contractor>(appDbContext, ValidationCodes.CONTRACTOR_NOT_FOUND, 
                    ValidationMessages.NotFound));

            RuleFor(x => x.Data.FileDto)
                .NotEmpty()
                .WithErrorCode(ValidationCodes.NULL_OR_EMPTY_INPUT)
                .WithMessage(ValidationMessages.InvalidInput);
        }
    }
}
