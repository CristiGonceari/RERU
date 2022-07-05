using CODWER.RERU.Core.DataTransferObjects.Address;
using MediatR;

namespace CODWER.RERU.Core.Application.Addresses.AddAddress
{
    public class AddAddressCommand : IRequest<int>
    {
        public AddressDto Data { get; set; }
    }
}
