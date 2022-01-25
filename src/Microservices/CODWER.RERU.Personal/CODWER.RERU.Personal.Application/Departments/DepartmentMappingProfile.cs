using AutoMapper;
using CODWER.RERU.Personal.Data.Entities;
using CODWER.RERU.Personal.DataTransferObjects.Departments;
using CVU.ERP.Common.DataTransferObjects.SelectValues;

namespace CODWER.RERU.Personal.Application.Departments
{
    public class DepartmentMappingProfile : Profile
    {
        public DepartmentMappingProfile()
        {
            CreateMap<AddEditDepartmentDto, Department>()
                .ForMember(x => x.Id, opts => opts.Ignore());

            CreateMap<Department, DepartmentDto>();

            CreateMap<Department, SelectItem>()
                .ForMember(x => x.Value, opts => opts.MapFrom(op => op.Id.ToString()))
                .ForMember(x => x.Label, opts => opts.MapFrom(op => op.Name));
        }
    }
}
