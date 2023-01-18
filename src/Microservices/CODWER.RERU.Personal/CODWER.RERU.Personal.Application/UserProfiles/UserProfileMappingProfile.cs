using AutoMapper;
using CODWER.RERU.Personal.DataTransferObjects.UserProfiles;
using RERU.Data.Entities;

namespace CODWER.RERU.Personal.Application.UserProfiles
{
    public class UserProfileMappingProfile : Profile
    {
        public UserProfileMappingProfile()
        {
            CreateMap<UserProfile, UserProfileDto>()
                .ForMember(r => r.UserId, opts => opts.MapFrom(op => op.Id))
                .ForMember(r => r.ContractorId, opts => opts.MapFrom(op => op.Contractor.Id));
        }
    }
}
