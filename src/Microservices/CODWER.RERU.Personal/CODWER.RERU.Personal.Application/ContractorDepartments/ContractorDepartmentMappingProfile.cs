using AutoMapper;
using CODWER.RERU.Personal.Data.Entities;
using CODWER.RERU.Personal.DataTransferObjects.ContractorDepartments;

namespace CODWER.RERU.Personal.Application.ContractorDepartments
{
    public class ContractorDepartmentMappingProfile : Profile
    {
        public ContractorDepartmentMappingProfile()
        {
            CreateMap<AddEditContractorDepartmentDto, ContractorDepartment>()
                .ForMember(x => x.Id, opts => opts.Ignore())
                .ForMember(x => x.Contractor, opts => opts.Ignore())
                .ForMember(x => x.Department, opts => opts.Ignore());

            CreateMap<ContractorDepartment, ContractorDepartmentDto>()
                .ForMember(x => x.ContractorName, opts => opts.MapFrom(op => $"{op.Contractor.FirstName} {op.Contractor.LastName}"))
                .ForMember(x => x.DepartmentName, opts => opts.MapFrom(op => op.Department.Name));
        }
    }
}
