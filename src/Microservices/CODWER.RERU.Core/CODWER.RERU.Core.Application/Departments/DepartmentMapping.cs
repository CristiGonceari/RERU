using AutoMapper;
using CODWER.RERU.Core.DataTransferObjects.Departemnts;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using RERU.Data.Entities;
using RERU.Data.Entities.PersonalEntities;

namespace CODWER.RERU.Core.Application.Departments
{
    public class DepartmentMapping : Profile
    {
        public DepartmentMapping()
        {
            CreateMap<Department, DepartmentDto>();

            CreateMap<DepartmentDto, Department>()
                .ForMember(x => x.Id, opts => opts.Ignore());

            CreateMap<Department, SelectItem>()
                .ForMember(destination => destination.Value, options => options.MapFrom(src => src.ColaboratorId.ToString()))
                .ForMember(destination => destination.Label, options => options.MapFrom(src => src.Name));
        }
    }
    
}
