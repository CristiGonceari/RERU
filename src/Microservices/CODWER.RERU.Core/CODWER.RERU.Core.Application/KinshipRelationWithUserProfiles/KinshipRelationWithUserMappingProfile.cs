using AutoMapper;
using CODWER.RERU.Core.DataTransferObjects.KinshipRelationWithUserProfile;
using RERU.Data.Entities;

namespace CODWER.RERU.Core.Application.KinshipRelationWithUserProfiles
{
    public class KinshipRelationWithUserMappingProfile : Profile
    {
        public KinshipRelationWithUserMappingProfile()
        {
            CreateMap<KinshipRelationWithUserProfileDto, KinshipRelationWithUserProfile>()
                .ForMember(x => x.Id, opts => opts.Ignore());

            CreateMap<KinshipRelationWithUserProfile, KinshipRelationWithUserProfileDto>();
        }
    }
}
