using CODWER.RERU.Personal.Application.Addresses;
using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators.Bulletin;
using CODWER.RERU.Personal.DataTransferObjects.Bulletin;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Bulletins
{
    public class BulletinValidator : AbstractValidator<BulletinsDataDto>
    {
        public BulletinValidator()
        {
            RuleFor(x => x.Idnp)
                .SetValidator(new IdnpValidator());

            RuleFor(x => x.Series)
                .NotEmpty()
                .WithErrorCode(ValidationCodes.EMPTY_BULLETIN_SERIES)
                .WithMessage(ValidationMessages.InvalidInput);

            RuleFor(x => x.EmittedBy)
                .NotEmpty()
                .WithErrorCode(ValidationCodes.EMPTY_BULLETIN_EMITTER)
                .WithMessage(ValidationMessages.InvalidInput);

            RuleFor(x => x.BirthPlace)
                .SetValidator(new AddressValidator());

            RuleFor(x => x.LivingAddress)
                .SetValidator(new AddressValidator());

            RuleFor(x => x.ResidenceAddress)
                .SetValidator(new AddressValidator());
        }
    }
}
