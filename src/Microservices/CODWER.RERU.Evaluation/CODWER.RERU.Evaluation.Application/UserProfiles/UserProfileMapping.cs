﻿using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using CVU.ERP.Common.DataTransferObjects.Users;
using CVU.ERP.Module.Application.Models;
using CVU.ERP.Module.Application.Models.Internal;
using RERU.Data.Entities;

namespace CODWER.RERU.Evaluation.Application.UserProfiles
{
    public class UserProfileMapping : Profile
    {
        public UserProfileMapping()
        {
            CreateMap<AddEditUserProfileDto, UserProfile>()
                .ForMember(x => x.Id, opts => opts.Ignore());

            CreateMap<UserProfile, UserProfileDto>()
                .ForMember(x => x.DepartmentColaboratorId, opts => opts.MapFrom(src => src.DepartmentColaboratorId))
                .ForMember(x => x.RoleColaboratorId, opts => opts.MapFrom(src => src.RoleColaboratorId))
                .ForMember(x => x.UserStatusEnum, opts => opts.MapFrom(src => src.DepartmentColaboratorId == null && src.RoleColaboratorId == null ? UserStatusEnum.Candidate : UserStatusEnum.Employee));

            CreateMap<UserProfileDto, InternalUserProfileCreate>()
                .ForMember(x => x.Name, opts => opts.MapFrom(src => src.FirstName))
                .ForMember(x => x.Email, opts => opts.MapFrom(src => src.Email));

            CreateMap<UserProfileDto, UserProfile>()
                .ForMember(x => x.Id, opts => opts.Ignore());

            CreateMap<ApplicationUser, UserProfile>()
                .ForMember(x => x.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(x => x.FirstName, opts => opts.MapFrom(src => src.FirstName))
                .ForMember(x => x.Email, opts => opts.MapFrom(src => src.Email));

            CreateMap<UserProfile, SelectItem>()
                .ForMember(x => x.Value, opts => opts.MapFrom(u => u.Id))
                .ForMember(x => x.Label, opts => opts.MapFrom(u => u.FirstName + " " + u.LastName + " " + u.FatherName + " " + $"({u.Idnp})"));


            CreateMap<BaseUserProfile, UserProfile>()
                .ForMember(x => x.Id, opts=> opts.MapFrom(u => u.Id))
                .ForMember(x => x.FirstName, opts => opts.MapFrom(u => u.FirstName))
                .ForMember(x => x.LastName, opts => opts.MapFrom(u => u.LastName))
                .ForMember(x => x.FatherName, opts => opts.MapFrom(u => u.FatherName))
                .ForMember(x => x.Idnp, opts => opts.MapFrom(u => u.Idnp))
                .ForMember(x => x.Email, opts => opts.MapFrom(u => u.Email));
        }
    }
}