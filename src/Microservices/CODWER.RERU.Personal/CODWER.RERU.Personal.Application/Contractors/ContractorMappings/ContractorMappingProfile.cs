using AutoMapper;
using CODWER.RERU.Personal.Data.Entities;
using CODWER.RERU.Personal.Data.Entities.StaticExtensions;
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
                .ForMember(x => x.ContractorId, opts => opts.MapFrom(op=>op.ContractorId))
                .ForMember(x => x.AvatarBase64, opts => opts.MapFrom(op => ConvertToBase64(op.Avatar)));

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
                .ForMember(x => x.DepartmentName, opts => opts.ConvertUsing( new DepartmentNameConverter(), op=>op))
                .ForMember(x => x.OrganizationRoleName, opts => opts.ConvertUsing(new OrganizationRoleConverter(), op => op))
                .ForMember(x => x.EmployerState, opts => opts.ConvertUsing(new EmployerStateConverter(), op => op));

            CreateMap<Contractor, ContractorDetailsDto>()
                .ForMember(r => r.Id, opts => opts.MapFrom(op => op.Id))
                .ForMember(r => r.FirstName, opts => opts.MapFrom(op => op.FirstName))
                .ForMember(r => r.LastName, opts => opts.MapFrom(op => op.LastName))
                .ForMember(x => x.DepartmentName, opts => opts.ConvertUsing(new DepartmentNameConverter(), op => op))
                .ForMember(x => x.OrganizationRoleName, opts => opts.ConvertUsing(new OrganizationRoleConverter(), op => op))
                .ForMember(x => x.Contacts, opts => opts.MapFrom(op => op.Contacts))
                .ForMember(x => x.EmployerState, opts => opts.ConvertUsing(new EmployerStateConverter(), op => op))

                .ForMember(x => x.HasUserProfile, opts => opts.MapFrom(op => op.UserProfile != null))
                .ForMember(x => x.HasAvatar, opts => opts.MapFrom(op => op.Avatar.AvatarBase64.Any()))
                .ForMember(x => x.HasBulletin, opts => opts.MapFrom(op => op.Bulletin != null))
                .ForMember(x => x.HasStudies, opts => opts.MapFrom(op => op.Studies.Any()))
                .ForMember(x => x.HasPositions, opts => opts.MapFrom(op => op.Positions.Any()))
                .ForMember(x => x.HasCim, opts => opts.MapFrom(op => op.Contracts.Any()));

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