using AutoMapper;
using CODWER.RERU.Personal.DataTransferObjects.Address;
using RERU.Data.Entities;

namespace CODWER.RERU.Personal.Application.Addresses
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
