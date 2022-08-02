using CODWER.RERU.Core.Application.Validation;
using CODWER.RERU.Core.DataTransferObjects.Address;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Core.Application.Addresses
{
    public class AddressValidator : AbstractValidator<AddressDto>
    {
        public AddressValidator()
        {
            RuleFor(x => x)
                .NotNull()
                .WithMessage(ValidationMessages.InvalidInput)
                .WithErrorCode(ValidationCodes.EMPTY_ADDRESS);

            When(x => x != null, () =>
            {

                RuleFor(x => x.City).NotEmpty()
                    .WithErrorCode(ValidationCodes.INVALID_INPUT)
                    .WithMessage(ValidationMessages.InvalidInput);

                RuleFor(x => x.Country).NotEmpty()
                    .WithErrorCode(ValidationCodes.INVALID_INPUT)
                    .WithMessage(ValidationMessages.InvalidInput);

                RuleFor(x => x.Region).NotEmpty()
                    .WithErrorCode(ValidationCodes.INVALID_INPUT)
                    .WithMessage(ValidationMessages.InvalidInput);


                RuleFor(x => x.PostCode).NotEmpty()
                    .WithErrorCode(ValidationCodes.INVALID_INPUT)
                    .WithMessage(ValidationMessages.InvalidInput);

            });

            When(x => x.Apartment != null, () =>
            {
                RuleFor(x => x.Apartment).NotEmpty()
               .WithErrorCode(ValidationCodes.INVALID_INPUT)
               .WithMessage(ValidationMessages.InvalidInput);
            });

            When(x => x.Street != null, () =>
            {
                RuleFor(x => x.Street).NotEmpty()
                    .WithErrorCode(ValidationCodes.INVALID_INPUT)
                    .WithMessage(ValidationMessages.InvalidInput);
            });

            When(x => x.Building != null, () =>
            {
                RuleFor(x => x.Building).NotEmpty()
                     .WithErrorCode(ValidationCodes.INVALID_INPUT)
                     .WithMessage(ValidationMessages.InvalidInput);
            });
        }
    }
}
