using AutoMapper;
using CVU.ERP.Common.DataTransferObjects.Users;
using CODWER.RERU.Core.Data.Entities;
using CODWER.RERU.Core.DataTransferObjects.Users;
using CVU.ERP.Identity.Models;

namespace CODWER.RERU.Core.Application.Users
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<ERPIdentityUser, UserDto>();
            CreateMap<ERPIdentityUser, CreateUserDto>()
                .ForMember(destinationMember => destinationMember.EmailNotification, options => options.Ignore());
            CreateMap<ERPIdentityUser, UserPersonalDataDto>();
            CreateMap<ERPIdentityUser, UserDetailsDto>();

            CreateMap<UserDto, ERPIdentityUser>()
                .ForMember(destinationMember => destinationMember.LockoutEnabled, options => options.Ignore())
                .ForMember(destinationMember => destinationMember.Id, options => options.Ignore());

            CreateMap<CreateUserDto, ERPIdentityUser>()
                .ForMember(destinationMember => destinationMember.UserName, options => options.MapFrom(source => source.Email))
                .ForMember(destinationMember => destinationMember.LockoutEnabled, options => options.Ignore())
                .ForMember(destinationMember => destinationMember.Id, options => options.Ignore());

            CreateMap<CreateUserDto, UserProfile>()
                .ForMember(x => x.IsActive, opts => opts.MapFrom(x => true));

            CreateMap<UserPersonalDataDto, ERPIdentityUser>()
                .ForMember(destinationMember => destinationMember.Id, options => options.Ignore());

            CreateMap<ERPIdentityUser, EditUserDto>();
            CreateMap<EditUserDto, ERPIdentityUser>()
                .ForMember(destinationMember => destinationMember.Id, options => options.Ignore());

            CreateMap<UserProfile, EditUserPersonalDetailsDto>();
            CreateMap<EditUserPersonalDetailsDto, UserProfile>()
                .ForMember(destinationMember => destinationMember.Id, options => options.Ignore());
        }
    }
}