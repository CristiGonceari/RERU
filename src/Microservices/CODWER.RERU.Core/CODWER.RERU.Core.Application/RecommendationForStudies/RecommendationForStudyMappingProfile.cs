using AutoMapper;
using CODWER.RERU.Core.DataTransferObjects.RecommendationForStudyDto;
using RERU.Data.Entities;

namespace CODWER.RERU.Core.Application.RecommendationForStudies
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
