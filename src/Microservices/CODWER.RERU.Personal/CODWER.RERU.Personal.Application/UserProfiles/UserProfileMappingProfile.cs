using AutoMapper;
using CODWER.RERU.Personal.Data.Entities.User;
using CODWER.RERU.Personal.DataTransferObjects.UserProfiles;

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
