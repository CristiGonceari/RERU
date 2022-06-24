using AutoMapper;
using CODWER.RERU.Core.DataTransferObjects.KinshipRelation;
using RERU.Data.Entities;

namespace CODWER.RERU.Core.Application.KinshipRelations
{
    public class KinshipRelationMappingProfile : Profile
    {
        public KinshipRelationMappingProfile()
        {
            CreateMap<KinshipRelationDto, KinshipRelation>()
                .ForMember(x => x.Id, opts => opts.Ignore());

            CreateMap<KinshipRelation, KinshipRelationDto>();
              
        }
    }
}
