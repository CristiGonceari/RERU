using AutoMapper;
using CODWER.RERU.Core.DataTransferObjects.MilitaryObligation;
using RERU.Data.Entities;

namespace CODWER.RERU.Core.Application.MilitaryObligations
{
    public class MilitaryObligationMappingProfile : Profile
    {
        public MilitaryObligationMappingProfile()
        {
            CreateMap<MilitaryObligationDto, MilitaryObligation>()
               .ForMember(x => x.Id, opts => opts.Ignore());

            CreateMap<MilitaryObligation, MilitaryObligationDto>();
        }
    }
}
