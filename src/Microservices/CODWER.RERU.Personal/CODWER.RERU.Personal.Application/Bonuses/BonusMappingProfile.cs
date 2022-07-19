using AutoMapper;
using RERU.Data.Entities.PersonalEntities.ContractorEvents;
using RERU.Data.Entities.PersonalEntities.StaticExtensions;
using CODWER.RERU.Personal.DataTransferObjects.Bonuses;

namespace CODWER.RERU.Personal.Application.Bonuses
{
    public class BonusMappingProfile : Profile
    {
        public BonusMappingProfile()
        {
            CreateMap<AddEditBonusDto, Bonus>()
                .ForMember(x => x.Id, opts => opts.Ignore())
                .ForMember(x => x.Contractor, opts => opts.Ignore())
                ;

            CreateMap<Bonus, BonusDto>()
                .ForMember(x => x.ContractorName, opts => opts.MapFrom(op => op.Contractor.GetFullName()))
                ;
        }
    }
}
