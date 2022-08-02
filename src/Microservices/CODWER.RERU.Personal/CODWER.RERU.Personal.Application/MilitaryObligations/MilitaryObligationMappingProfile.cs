using AutoMapper;
using CODWER.RERU.Personal.DataTransferObjects.MilitaryObligation;
using RERU.Data.Entities;

namespace CODWER.RERU.Personal.Application.MilitaryObligations
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
