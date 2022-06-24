using CODWER.RERU.Core.Application.Addresses;
using CODWER.RERU.Core.Application.Validation;
using CODWER.RERU.Core.Application.Validators.IDNP;
using CODWER.RERU.Core.DataTransferObjects.Bulletin;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Core.Application.Bulletins
{
    public class BulletinValidator : AbstractValidator<BulletinDto>
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

            RuleFor(x => x.ParentsResidenceAddress)
                .SetValidator(new AddressValidator());

            RuleFor(x => x.ResidenceAddress)
                .SetValidator(new AddressValidator());
        }
    }
}
