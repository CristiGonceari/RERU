using AutoMapper;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Entities.PersonalEntities.StaticExtensions;
using CODWER.RERU.Personal.DataTransferObjects.Avatars;
using CODWER.RERU.Personal.DataTransferObjects.Contractors;
using CODWER.RERU.Personal.DataTransferObjects.Files;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using File = CVU.ERP.StorageService.Entities.File;

namespace CODWER.RERU.Personal.Application.Contractors.ContractorMappings
{
    public class ContractorMappingProfile : Profile
    {
        public ContractorMappingProfile()
        {
            CreateMap<ContractorAvatarDto, ContractorAvatar>()
                .ForMember(x => x.ContractorId, opts => opts.MapFrom(op=>op.ContractorId));

            CreateMap<AddEditContractorDto, Contractor>()
                .ForMember(r => r.Id, opts => opts.Ignore())
                .ForMember(r => r.Code, opts => opts.Ignore());
                //.ForAllOtherMembers(opts => opts.Ignore());

            CreateMap<Contractor, ContractorDto>()
                .ForMember(r => r.Id, opts => opts.MapFrom(op => op.Id))
                .ForMember(r => r.Code, opts => opts.MapFrom(op => op.Code))
                .ForMember(r => r.FirstName, opts => opts.MapFrom(op => op.FirstName))
                .ForMember(r => r.LastName, opts => opts.MapFrom(op => op.LastName))
                .ForMember(r => r.FatherName, opts => opts.MapFrom(op => op.FatherName))
                .ForMember(r => r.Contacts, opts => opts.MapFrom(op => op.Contacts))
                .ForMember(r => r.Idnp, opts => opts.MapFrom(op => op.Idnp))
                //.ForMember(x => x.DepartmentName, opts => opts.ConvertUsing( new DepartmentNameConverter(), op=>op))
                .ForMember(x => x.DepartmentId, opts => opts.MapFrom(op => op.UserProfile.Department.Id))
                .ForMember(x => x.DepartmentName, opts => opts.MapFrom(op => op.UserProfile.Department.Name))
                .ForMember(x => x.OrganizationRoleId, opts => opts.MapFrom(op => op.UserProfile.Role.Id))
                .ForMember(x => x.OrganizationRoleName, opts => opts.MapFrom(op => op.UserProfile.Role.Name))
                .ForMember(x => x.FunctionId, opts => opts.MapFrom(op => op.UserProfile.EmployeeFunction.Id))
                .ForMember(x => x.FunctionName, opts => opts.MapFrom(op => op.UserProfile.EmployeeFunction.Name))
                //.ForMember(x => x.OrganizationRoleName, opts => opts.ConvertUsing(new RoleConverter(), op => op))
                .ForMember(x => x.EmployerState, opts => opts.ConvertUsing(new EmployerStateConverter(), op => op));

            CreateMap<Contractor, ContractorDetailsDto>()
                .ForMember(r => r.Id, opts => opts.MapFrom(op => op.Id))
                .ForMember(r => r.FirstName, opts => opts.MapFrom(op => op.FirstName))
                .ForMember(r => r.LastName, opts => opts.MapFrom(op => op.LastName))
                .ForMember(r => r.Idnp, opts => opts.MapFrom(op => op.Idnp))
                .ForMember(x => x.DepartmentName, opts => opts.MapFrom(op => op.UserProfile.Department.Name))
                //.ForMember(x => x.DepartmentName, opts => opts.ConvertUsing(new DepartmentNameConverter(), op => op))
                .ForMember(x => x.OrganizationRoleName, opts => opts.MapFrom(op => op.UserProfile.Role.Name))
                .ForMember(x => x.FunctionName, opts => opts.MapFrom(op => op.UserProfile.EmployeeFunction.Name))
                //.ForMember(x => x.OrganizationRoleName, opts => opts.ConvertUsing(new RoleConverter(), op => op))
                .ForMember(x => x.Contacts, opts => opts.MapFrom(op => op.Contacts))
                .ForMember(x => x.EmployerState, opts => opts.ConvertUsing(new EmployerStateConverter(), op => op))
                .ForMember(x => x.UserState, opts => opts.ConvertUsing(new UserStateConverter(), op => op))

                .ForMember(x => x.HasAutobiography, opts => opts.MapFrom(op => op.Autobiography != null))
                .ForMember(x => x.HasMilitaryObligations, opts => opts.MapFrom(op => op.MilitaryObligations.Any()))
                .ForMember(x => x.HasRecommendationsForStudy, opts => opts.MapFrom(op => op.RecommendationForStudies.Any()))
                .ForMember(x => x.HasMaterialStatus, opts => opts.MapFrom(op => op.MaterialStatus != null))
                .ForMember(x => x.HasKinshipRelations, opts => opts.MapFrom(op => op.KinshipRelations.Any()))
                .ForMember(x => x.HasKinshipRelationCriminalData, opts => opts.MapFrom(op => op.KinshipRelationCriminalData != null))
                .ForMember(x => x.HasKinshipRelationWithUserProfiles, opts => opts.MapFrom(op => op.KinshipRelationWithUserProfiles.Any()))
                .ForMember(x => x.HasModernLanguages, opts => opts.MapFrom(op => op.ModernLanguageLevels.Any()))
                .ForMember(x => x.HasUserProfile, opts => opts.MapFrom(op => op.UserProfile != null))
                .ForMember(x => x.HasAvatar, opts => opts.MapFrom(op => op.Avatar.MediaFileId.Any()))
                .ForMember(x => x.HasBulletin, opts => opts.MapFrom(op => op.Bulletin != null))
                .ForMember(x => x.HasStudies, opts => opts.MapFrom(op => op.Studies.Any()))
                .ForMember(x => x.HasPositions, opts => opts.MapFrom(op => op.Positions.Any()))
                .ForMember(x => x.HasCim, opts => opts.MapFrom(op => op.Contracts.Any()));

            CreateMap<Contractor, CandidateContractorStepsDto>()
                .ForMember(x => x.ContractorId, opts => opts.MapFrom(src => src.Id));

            CreateMap<ContractorAvatar, ContractorAvatarDetailsDto>();
                
            //.ForMember(x => x.ContractorFiles, opts => opts.MapFrom(op => op.ByteArrayFiles.Where(x=>x.FileType == FileTypeEnum.Identity).ToList()));

            CreateMap<File, FileNameDto>()
                .ForMember(x => x.Id, opts => opts.MapFrom(op => op.Id))
                .ForMember(x => x.Name, opts => opts.MapFrom(op => $"{op.FileName}"));


            CreateMap<AddEditContractorName, Contractor>()
                .ForMember(x => x.Id, opts => opts.Ignore())
                .ForMember(x => x.FirstName, opts => opts.MapFrom(x => x.FirstName))
                .ForMember(x => x.LastName, opts => opts.MapFrom(x => x.LastName));
                //.ForAllOtherMembers(opts => opts.Ignore());

            CreateMap<Contractor, ContractorSelectItem>()
                .ForMember(x => x.Value, opts => opts.MapFrom(op => op.Id))
                .ForMember(x => x.Label, opts => opts.MapFrom(op => op.GetFullName()))
                .ForMember(x => x.Properties, opts => opts.ConvertUsing(new ContractorSelectItemConverter(), op => op));

            CreateMap<Contractor, SelectItem>()
                .ForMember(x => x.Value, opts => opts.MapFrom(op => op.Id))
                .ForMember(x => x.Label, opts => opts.MapFrom(op => op.GetFullName()));

            CreateMap<ContractorLocalPermissionsDto, ContractorLocalPermission>();

            CreateMap<ContractorLocalPermission, ContractorLocalPermissionsDto>();
        }
        private static string ConvertToBase64(IFormFile file)
        {
            if (file.Length > 0)
            {
                using var ms = new MemoryStream();
                file.CopyTo(ms);
                var fileBytes = ms.ToArray();
                string base64String = Convert.ToBase64String(fileBytes);

                return $"data:image/png;base64,{base64String}";
            }

            return "";
        }
    }
}