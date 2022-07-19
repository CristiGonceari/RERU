using AutoMapper;
using RERU.Data.Entities.PersonalEntities.ContractorEvents;
using CODWER.RERU.Personal.DataTransferObjects.Ranks;

namespace CODWER.RERU.Personal.Application.Ranks
{
    public class RankMappingProfile : Profile
    {
        public RankMappingProfile()
        {
            CreateMap<AddEditRankDto, Rank>()
                .ForMember(x => x.Id, opts => opts.Ignore())
                .ForMember(x => x.Contractor, opts => opts.Ignore());

            CreateMap<Rank, RankDto>()
                .ForMember(x => x.RankRecordName, opts => opts.MapFrom(op => op.RankRecord.Name));
        }
    }
}
