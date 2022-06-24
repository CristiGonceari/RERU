using CODWER.RERU.Core.DataTransferObjects.Address;
using MediatR;

namespace CODWER.RERU.Core.Application.Addresses.GetAddress
{
    public class GetAddressQuery : IRequest<AddressDto>
    {
        public int Id { get; set; }
    }
}
