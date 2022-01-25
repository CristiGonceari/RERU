using AutoMapper;
using CODWER.RERU.Personal.Data.Entities;
using CODWER.RERU.Personal.DataTransferObjects.Address;

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
