using AutoMapper;
using CODWER.RERU.Personal.DataTransferObjects.UserProfiles;
using RERU.Data.Entities;

namespace CODWER.RERU.Personal.Application.UserProfiles
{
    public class UserProfileMappingProfile : Profile
    {
        public UserProfileMappingProfile()
        {
            CreateMap<UserProfile, UserProfileDto>();
        }
    }
}
