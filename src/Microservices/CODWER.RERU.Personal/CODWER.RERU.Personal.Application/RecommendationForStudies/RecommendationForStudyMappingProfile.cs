using AutoMapper;
using CODWER.RERU.Personal.DataTransferObjects.RecommendationForStudy;
using RERU.Data.Entities;

namespace CODWER.RERU.Personal.Application.RecommendationForStudies
{
    public class RecommendationForStudyMappingProfile : Profile
    {
        public RecommendationForStudyMappingProfile()
        {
            CreateMap<RecommendationForStudyDto, RecommendationForStudy>()
                .ForMember(x => x.Id, opts => opts.Ignore());

            CreateMap<RecommendationForStudy, RecommendationForStudyDto>();
        }
    }
}
