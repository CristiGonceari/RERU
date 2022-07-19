using AutoMapper;
using RERU.Data.Entities.PersonalEntities;
using CODWER.RERU.Personal.DataTransferObjects.Contracts;

namespace CODWER.RERU.Personal.Application.Contracts
{
    public class ContractMappingProfile : Profile
    {
        public ContractMappingProfile()
        {
            CreateMap<AddIndividualContractDto, IndividualContract>()
                .ForMember(x => x.Id, opts => opts.Ignore());

            CreateMap<IndividualContract, IndividualContractDetails>()
                .ForMember(x => x.Instructions, opts => opts.Ignore());

            CreateMap<UpdateIndividualContractDto, IndividualContract>()
                .ForMember(x => x.Id, opts => opts.Ignore())
                .ForMember(x => x.No, opts => opts.Ignore());
        }
    }
}
