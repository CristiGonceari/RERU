using AutoMapper;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using RERU.Data.Entities;
using RERU.Data.Entities.PersonalEntities;

namespace CODWER.RERU.Evaluation360.Application.Departments
{
    public class DepartmentMapping : Profile
    {
        public DepartmentMapping()
        {
            CreateMap<Department, SelectItem>()
                .ForMember(destination => destination.Value, options => options.MapFrom(src => src.ColaboratorId.ToString()))
                .ForMember(destination => destination.Label, options => options.MapFrom(src => src.Name));

            CreateMap<Role, SelectItem>()
                .ForMember(destination => destination.Value, options => options.MapFrom(src => src.Id.ToString()))
                .ForMember(destination => destination.Label, options => options.MapFrom(src => src.Name));

            CreateMap<EmployeeFunction, SelectItem>()
                .ForMember(destination => destination.Value, options => options.MapFrom(src => src.Id.ToString()))
                .ForMember(destination => destination.Label, options => options.MapFrom(src => src.Name));
        }
    }
}
