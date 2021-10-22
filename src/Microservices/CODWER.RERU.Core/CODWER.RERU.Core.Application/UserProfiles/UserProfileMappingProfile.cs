using System.Linq;
using AutoMapper;
using CODWER.RERU.Core.Data.Entities;
using CODWER.RERU.Core.DataTransferObjects.Profile;
using CODWER.RERU.Core.DataTransferObjects.UserProfiles;
using CODWER.RERU.Core.DataTransferObjects.Users;
using CVU.ERP.Module.Application.Models;
using CVU.ERP.Module.Application.Models.Internal;

namespace CODWER.RERU.Core.Application.UserProfiles
{
    public class UserProfileMappingProfile : Profile
    {
        public UserProfileMappingProfile()
        {
            CreateMap<Data.Entities.UserProfile, UserProfileDto>();

            CreateMap<UserProfileDto, Data.Entities.UserProfile>()
                .ForMember(x => x.Id, options => options.Ignore());

            CreateMap<Data.Entities.UserProfile, UserForRemoveDto>();

            CreateMap<UserForRemoveDto, Data.Entities.UserProfile>();

            CreateMap<UserProfile, ApplicationUser>()
                .ForMember(destinationMember => destinationMember.Id, opts => opts.MapFrom(sourceMember => sourceMember.Id.ToString()))
                .ForMember(destinationMember => destinationMember.Name, opts => opts.MapFrom(sourceMember => $"{sourceMember.Name} {sourceMember.LastName}"))
                .ForMember(destinationMember => destinationMember.Modules, opts => opts.MapFrom(sourceMember => sourceMember.ModuleRoles));
            //.ForMember (destinationMember => destinationMember.Permissions, opts => opts.MapFrom (sourceMember => sourceMember.ModuleRoles.SelectMany (mr => mr.ModuleRole.Permissions.Select (p => p.Permission.Code))));

            CreateMap<UserProfileModuleRole, ApplicationUserModule>()
                .ForMember(destinationMember => destinationMember.Role, opts => opts.MapFrom(sourceMember => sourceMember.ModuleRole.Name))
                .ForMember(destinationMember => destinationMember.Module, opts => opts.MapFrom(sourceMember => sourceMember.ModuleRole.Module))
                .ForMember(destinationMember => destinationMember.Permissions, opts => opts.MapFrom(sourceMember => sourceMember.ModuleRole.Permissions.Select(p => p.Permission.Code)));

            CreateMap<Data.Entities.Module, ApplicationModule>();

            CreateMap<UserProfile, UserProfileOverviewDto>()
                .ForMember(destinationMember => destinationMember.Modules, opts => opts.MapFrom(sourceMember => sourceMember.ModuleRoles));

            CreateMap<UserProfileModuleRole, UserProfileModuleRowDto>()
                .ForMember(destinationMember => destinationMember.Role, opts => opts.MapFrom(sourceMember => sourceMember.ModuleRole.Name))
                .ForMember(destinationMember => destinationMember.Module, opts => opts.MapFrom(sourceMember => sourceMember.ModuleRole.Module.Name))
                .ForMember(destinationMember => destinationMember.ModuleIcon, opts => opts.MapFrom(sourceMember => sourceMember.ModuleRole.Module.Icon))
                .ForMember(destinationMember => destinationMember.ModulePublicUrl, opts => opts.MapFrom(sourceMember => sourceMember.ModuleRole.Module.PublicUrl));

            CreateMap<UserProfile, UserDetailsOverviewDto>();

            CreateMap<InternalUserProfileCreate, UserProfile>()
            .ForMember(x => x.IsActive, opts => opts.MapFrom(x => true))
            .ForMember(x => x.ModuleRoles, opts => opts.Ignore());
        }
    }
}