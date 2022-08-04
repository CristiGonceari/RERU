using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Entities.PersonalEntities.Enums;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Contacts;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Contacts
{
    public class ContactValidator : AbstractValidator<AddEditContactDto>
    {
        public ContactValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Type)
                .IsInEnum()
                .WithMessage(ValidationMessages.InvalidInput)
                .WithErrorCode(ValidationCodes.INVALID_INPUT);

            RuleFor(x => x.Value)
                .NotEmpty()
                .WithMessage(ValidationMessages.InvalidInput)
                .WithErrorCode(ValidationCodes.INVALID_INPUT);

            When(x => x.Type == ContactTypeEnum.Email, () =>
            {
                RuleFor(x=>x.Value).EmailAddress()
                    .WithMessage(ValidationMessages.InvalidInput)
                    .WithErrorCode(ValidationCodes.INVALID_INPUT);
            });


            When(x => x.Type == ContactTypeEnum.PhoneNumber, () =>
            {
                RuleFor(x => x.Value)
                    .Matches("^[0-9]*$")
                    .Length(8, 13)
                    .WithMessage(ValidationMessages.InvalidInput)
                    .WithErrorCode(ValidationCodes.INVALID_INPUT);
            });

            RuleFor(x => x.ContractorId)
                .SetValidator(new ItemMustExistValidator<Contractor>(appDbContext, ValidationCodes.CONTRACTOR_NOT_FOUND, ValidationMessages.InvalidReference));
        }
    }
}
