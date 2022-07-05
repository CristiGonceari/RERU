using CODWER.RERU.Core.DataTransferObjects.Address;
using MediatR;

namespace CODWER.RERU.Core.Application.Addresses.UpdateAddress
{
    public class UpdateAddressCommand : IRequest<Unit>
    {
        public AddressDto Data { get; set; }
    }
}
