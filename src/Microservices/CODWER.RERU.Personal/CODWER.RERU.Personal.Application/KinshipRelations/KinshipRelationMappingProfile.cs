using AutoMapper;
using CODWER.RERU.Personal.DataTransferObjects.KinshipRelation;
using RERU.Data.Entities;

namespace CODWER.RERU.Personal.Application.KinshipRelations
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
