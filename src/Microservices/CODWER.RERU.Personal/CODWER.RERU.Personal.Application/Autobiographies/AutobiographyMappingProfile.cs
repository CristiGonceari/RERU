using AutoMapper;
using CODWER.RERU.Personal.DataTransferObjects.Autobiography;
using RERU.Data.Entities;

namespace CODWER.RERU.Personal.Application.Autobiographies
{
    public class AutobiographyMappingProfile : Profile
    {
        public AutobiographyMappingProfile()
        {
            CreateMap<AutobiographyDto, Autobiography>()
                           .ForMember(x => x.Id, opts => opts.Ignore());

            CreateMap<Autobiography, AutobiographyDto>();
        }
    }
}
