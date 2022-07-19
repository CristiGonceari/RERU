using AutoMapper;
using RERU.Data.Entities.PersonalEntities;
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
