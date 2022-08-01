using AutoMapper;
using CODWER.RERU.Personal.DataTransferObjects.KinshipRelationWithCriminalData;
using RERU.Data.Entities;

namespace CODWER.RERU.Personal.Application.KinshipRelationWithCriminalDatas
{
    public class KinshipRelationWithCriminalDataMappingProfile : Profile
    {
        public KinshipRelationWithCriminalDataMappingProfile()
        {
            CreateMap<KinshipRelationWithCriminalDataDto, KinshipRelationCriminalData>()
                .ForMember(x => x.Id, opts => opts.Ignore());

            CreateMap<KinshipRelationCriminalData, KinshipRelationWithCriminalDataDto>();
        }
    }
}
