using AutoMapper;
using CODWER.RERU.Core.DataTransferObjects.Address;
using RERU.Data.Entities;

namespace CODWER.RERU.Core.Application.Addresses
{
    public class AddressMappingProfile : Profile
    {
        public AddressMappingProfile()
        {
            CreateMap<AddressDto, Address>()
                .ForMember(r => r.Id, opts => opts.Ignore());

            CreateMap<Address, AddressDto>();
        }
    }
}
