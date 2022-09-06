﻿using System.Linq;
using AutoMapper;
using CODWER.RERU.Core.DataTransferObjects.Profile;
using CODWER.RERU.Core.DataTransferObjects.UserProfiles;
using CODWER.RERU.Core.DataTransferObjects.Users;
using CVU.ERP.Common.DataTransferObjects.Users;
using CVU.ERP.Module.Application.Models;
using CVU.ERP.Module.Application.Models.Internal;
using CVU.ERP.ServiceProvider.Models;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Core.Application.UserProfiles
{
    public class UserProfileMappingProfile : Profile
    {
        public UserProfileMappingProfile()
        {
            CreateMap<UserProfile, UserProfileDto>()
                .ForMember(x => x.DepartmentName, opts => opts.MapFrom(src => src.Department.Name))
                .ForMember(x => x.RoleName, opts => opts.MapFrom(src => src.Role.Name))
                .ForMember(x => x.UserStatusEnum, opts => opts.MapFrom(src => src.DepartmentColaboratorId == null && src.RoleColaboratorId == null ? UserStatusEnum.Candidate : UserStatusEnum.Employee))
                .ForMember(x => x.AccessModeEnum, opts => opts.MapFrom(src => src.AccessModeEnum != null ? src.AccessModeEnum : AccessModeEnum.OnlyCandidates))
                .ForMember(x => x.UserName, opts => opts.MapFrom(src => src.LastName + " " + src.FirstName + " " + src.FatherName));

            CreateMap<UserProfile, CandidateProfileDto>()
                .ForMember(x => x.DepartmentName, opts => opts.MapFrom(src => src.Department.Name))
                .ForMember(x => x.RoleName, opts => opts.MapFrom(src => src.Role.Name))
                .ForMember(x => x.BirthDate, opts => opts.MapFrom(src => src.BirthDate))
                .ForMember(x => x.UserStatusEnum, opts => opts.MapFrom(src => src.DepartmentColaboratorId == null && src.RoleColaboratorId == null ? UserStatusEnum.Candidate : UserStatusEnum.Employee))
                .ForMember(x => x.AccessModeEnum, opts => opts.MapFrom(src => src.AccessModeEnum != null ? src.AccessModeEnum : AccessModeEnum.OnlyCandidates))
                .ForMember(x => x.UserName, opts => opts.MapFrom(src => src.LastName + " " + src.FirstName + " " + src.FatherName))
                .ForMember(x => x.BulletinId, opts => opts.MapFrom(src => src.Contractor.Bulletin.Id))
                .ForMember(x => x.StudyCount, opts => opts.MapFrom(src => src.Contractor.Studies.Count()))
                .ForMember(x => x.ModernLanguageLevelsCount, opts => opts.MapFrom(src => src.Contractor.ModernLanguageLevels.Count()))
                .ForMember(x => x.RecomendationsForStudyCount, opts => opts.MapFrom(src => src.Contractor.RecommendationForStudies.Count()))
                .ForMember(x => x.MaterialStatusId, opts => opts.MapFrom(src => src.Contractor.MaterialStatus.Id))
                .ForMember(x => x.KinshipRelationsCount, opts => opts.MapFrom(src => src.Contractor.KinshipRelations.Count()))
                .ForMember(x => x.KinshipRelationCriminalDataId, opts => opts.MapFrom(src => src.Contractor.KinshipRelationCriminalData.Id))
                .ForMember(x => x.KinshipRelationWithUserProfilesCount, opts => opts.MapFrom(src => src.Contractor.KinshipRelationWithUserProfiles.Count()))
                .ForMember(x => x.MilitaryObligationsCount, opts => opts.MapFrom(src => src.Contractor.MilitaryObligations.Count()))
                .ForMember(x => x.ContractorId, opts => opts.MapFrom(src => src.Contractor.Id))
                .ForMember(x => x.AutobiographyId, opts => opts.MapFrom(src => src.Contractor.Autobiography.Id));


            CreateMap<UserProfile, EditCandidateDto>()
                .ForMember(x => x.CandidateNationalityId, opts => opts.MapFrom(src => src.Contractor.CandidateNationalityId))
                .ForMember(x => x.CandidateCitizenshipId, opts => opts.MapFrom(src => src.Contractor.CandidateCitizenshipId))
                .ForMember(x => x.Sex, opts => opts.MapFrom(src => src.Contractor.Sex))
                .ForMember(x => x.StateLanguageLevel, opts => opts.MapFrom(src => src.Contractor.StateLanguageLevel))
                .ForMember(x => x.WorkPhone, opts => opts.MapFrom(src => src.Contractor.WorkPhone))
                .ForMember(x => x.HomePhone, opts => opts.MapFrom(src => src.Contractor.HomePhone));

            CreateMap<UserProfileDto, UserProfile>()
                .ForMember(x => x.Id, options => options.Ignore());

            CreateMap<CandidateProfileDto, UserProfile>()
                .ForMember(x => x.Id, options => options.Ignore());

            CreateMap<UserProfile, CandidateRegistrationStepsDto>()
                .ForMember(x => x.UserProfileId, opts => opts.MapFrom(src => src.Id));

            CreateMap<UserProfile, UserForRemoveDto>();

            CreateMap<UserForRemoveDto, UserProfile>();

            CreateMap<UserProfile, ApplicationUser>()
                .ForMember(destinationMember => destinationMember.Id, opts => opts.MapFrom(sourceMember => sourceMember.Id.ToString()))
                .ForMember(destinationMember => destinationMember.FirstName, opts => opts.MapFrom(sourceMember => sourceMember.FirstName))
                .ForMember(destinationMember => destinationMember.LastName, opts => opts.MapFrom(sourceMember => sourceMember.LastName))
                .ForMember(destinationMember => destinationMember.FatherName, opts => opts.MapFrom(sourceMember => sourceMember.FatherName))
                .ForMember(destinationMember => destinationMember.Idnp, opts => opts.MapFrom(sourceMember => sourceMember.Idnp))
                .ForMember(destinationMember => destinationMember.Email, opts => opts.MapFrom(sourceMember => sourceMember.Email))
                .ForMember(destinationMember => destinationMember.Modules, opts => opts.MapFrom(sourceMember => sourceMember.ModuleRoles));
            //.ForMember (destinationMember => destinationMember.Permissions, opts => opts.MapFrom (sourceMember => sourceMember.ModuleRoles.SelectMany (mr => mr.ModuleRole.Permissions.Select (p => p.Permission.Code))));

            CreateMap<UserProfileModuleRole, ApplicationUserModule>()
                .ForMember(destinationMember => destinationMember.Role, opts => opts.MapFrom(sourceMember => sourceMember.ModuleRole.Name))
                .ForMember(destinationMember => destinationMember.Module, opts => opts.MapFrom(sourceMember => sourceMember.ModuleRole.Module))
                .ForMember(destinationMember => destinationMember.Permissions, opts => opts.MapFrom(sourceMember => sourceMember.ModuleRole.Permissions.Select(p => p.Permission.Code)));

            CreateMap<global::RERU.Data.Entities.Module, ApplicationModule>();

            CreateMap<UserProfile, UserProfileOverviewDto>()
                .ForMember(x => x.UserStatusEnum, opts => opts.MapFrom(src => src.DepartmentColaboratorId == null && src.RoleColaboratorId == null ? UserStatusEnum.Candidate : UserStatusEnum.Employee))
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

            CreateMap<UserProfile, BaseUserProfile>()
                .ForMember(destinationMember => destinationMember.Id, opts => opts.MapFrom(sourceMember => sourceMember.Id.ToString()))
                .ForMember(destinationMember => destinationMember.FirstName, opts => opts.MapFrom(sourceMember => sourceMember.FirstName))
                .ForMember(destinationMember => destinationMember.LastName, opts => opts.MapFrom(sourceMember => sourceMember.LastName))
                .ForMember(destinationMember => destinationMember.FatherName, opts => opts.MapFrom(sourceMember => sourceMember.FatherName))
                .ForMember(destinationMember => destinationMember.Email, opts => opts.MapFrom(sourceMember => sourceMember.Email))
                .ForMember(destinationMember => destinationMember.Idnp, opts => opts.MapFrom(sourceMember => sourceMember.Idnp));
        }
    }
}