using AutoMapper;
using CODWER.RERU.Personal.DataTransferObjects.KinshipRelationWithUserProfile;
using RERU.Data.Entities;

namespace CODWER.RERU.Personal.Application.KinshipRelationWithUserProfiles
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
