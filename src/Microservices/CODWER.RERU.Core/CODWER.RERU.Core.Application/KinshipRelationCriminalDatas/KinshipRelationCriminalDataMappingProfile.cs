using AutoMapper;
using CODWER.RERU.Core.DataTransferObjects.KinshipRelationCriminalData;
using RERU.Data.Entities;

namespace CODWER.RERU.Core.Application.KinshipRelationCriminalDatas
{
    public class KinshipRelationCriminalDataMappingProfile : Profile
    {
        public KinshipRelationCriminalDataMappingProfile()
        {
            CreateMap<KinshipRelationCriminalDataDto, KinshipRelationCriminalData>()
                .ForMember(x => x.Id, opts => opts.Ignore());

            CreateMap<KinshipRelationCriminalData, KinshipRelationCriminalDataDto>();
        }
    }
}
