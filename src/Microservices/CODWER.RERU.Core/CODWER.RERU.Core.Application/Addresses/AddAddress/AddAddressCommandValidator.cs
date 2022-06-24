using FluentValidation;

namespace CODWER.RERU.Core.Application.Addresses.AddAddress
{
    public class AddAddressCommandValidator : AbstractValidator<AddAddressCommand>
    {
        public AddAddressCommandValidator()
        {
            RuleFor(x => x.Data).SetValidator(new AddressValidator());

        }
    }
}
