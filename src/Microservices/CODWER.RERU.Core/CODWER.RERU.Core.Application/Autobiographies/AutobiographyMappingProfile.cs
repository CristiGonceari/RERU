using AutoMapper;
using CODWER.RERU.Core.DataTransferObjects.Autobiography;
using RERU.Data.Entities;

namespace CODWER.RERU.Core.Application.Autobiographies
{
    public class AutobiographyMappingProfile : Profile
    {
        public AutobiographyMappingProfile()
        {
            CreateMap<AutobiographyDto, Autobiography>()
                           .ForMember(x => x.Id, opts => opts.Ignore());

            CreateMap<Bulletin, AutobiographyDto>();
        }
    }
}
