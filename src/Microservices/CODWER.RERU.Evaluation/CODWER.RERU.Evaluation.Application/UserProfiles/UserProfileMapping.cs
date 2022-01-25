using AutoMapper;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using CVU.ERP.Module.Application.Models;
using CVU.ERP.Module.Application.Models.Internal;

namespace CODWER.RERU.Evaluation.Application.UserProfiles
{
    public class UserProfileMapping : Profile
    {
        public UserProfileMapping()
        {
            CreateMap<AddEditUserProfileDto, UserProfile>()
                .ForMember(x => x.Id, opts => opts.Ignore());

            CreateMap<UserProfile, UserProfileDto>();

            CreateMap<UserProfileDto, InternalUserProfileCreate>()
                .ForMember(x => x.Name, opts => opts.MapFrom(src => src.FirstName))
                .ForMember(x => x.Email, opts => opts.MapFrom(src => src.Email));

            CreateMap<UserProfileDto, UserProfile>()
                .ForMember(x => x.Id, opts => opts.Ignore());

            CreateMap<ApplicationUser, UserProfile>()
                .ForMember(x => x.Id, opts => opts.Ignore())
                .ForMember(x => x.CoreUserId, opts => opts.MapFrom(src => src.Id))
                .ForMember(x => x.FirstName, opts => opts.MapFrom(src => src.Name))
                .ForMember(x => x.Email, opts => opts.MapFrom(src => src.Email));

            CreateMap<UserProfile, SelectItem>()
                .ForMember(x => x.Value, opts => opts.MapFrom(u => u.Id))
                .ForMember(x => x.Label, opts => opts.MapFrom(u => u.FirstName + " " + u.LastName));
        }
    }
}