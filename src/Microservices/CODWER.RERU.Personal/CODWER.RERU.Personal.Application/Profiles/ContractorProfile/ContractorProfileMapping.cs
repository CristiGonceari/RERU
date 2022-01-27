using System.Linq;
using AutoMapper;
using CODWER.RERU.Personal.Application.Contractors.ContractorMappings;
using CODWER.RERU.Personal.Data.Entities;
using CODWER.RERU.Personal.Data.Entities.Files;
using CODWER.RERU.Personal.DataTransferObjects.Profiles;

namespace CODWER.RERU.Personal.Application.Profiles.ContractorProfile
{
    public class ContractorProfileMapping : Profile
    {
        public ContractorProfileMapping()
        {
            CreateMap<Contractor, ContractorProfileDto>()
                .ForMember(r => r.Id, opts => opts.MapFrom(op => op.Id))
                .ForMember(r => r.FirstName, opts => opts.MapFrom(op => op.FirstName))
                .ForMember(r => r.LastName, opts => opts.MapFrom(op => op.LastName))
                .ForMember(x => x.DepartmentName, opts => opts.ConvertUsing(new DepartmentNameConverter(), op => op))
                .ForMember(x => x.OrganizationRoleName,
                    opts => opts.ConvertUsing(new OrganizationRoleConverter(), op => op))
                .ForMember(x => x.Contacts, opts => opts.MapFrom(op => op.Contacts))
                .ForMember(x => x.EmployerState, opts => opts.ConvertUsing(new EmployerStateConverter(), op => op))

                .ForMember(x => x.HasUserProfile, opts => opts.MapFrom(op => op.UserProfile != null))
                .ForMember(x => x.HasEmploymentRequest, opts => opts.MapFrom(op => op.ByteArrayFiles.Any(x => x.Type == FileTypeEnum.Request)))
                .ForMember(x => x.HasBulletin, opts => opts.MapFrom(op => op.Bulletin != null))
                .ForMember(x => x.HasStudies, opts => opts.MapFrom(op => op.Studies.Any()))
                .ForMember(x => x.HasIdentityDocuments, opts => opts.MapFrom(op => op.ByteArrayFiles.Any(x => x.Type == FileTypeEnum.Identity)))
                .ForMember(x => x.HasPositions, opts => opts.MapFrom(op => op.Positions.Any()))
                .ForMember(x => x.HasCim, opts => opts.MapFrom(op => op.Contracts.Any()));



        }
    }
}
