using AutoMapper;
using CODWER.RERU.Core.DataTransferObjects.Departemnts;
using RERU.Data.Entities;

namespace CODWER.RERU.Core.Application.Departments
{
    public class DepartmentMapping : Profile
    {
        public DepartmentMapping()
        {
            CreateMap<Department, DepartmentDto>();

            CreateMap<DepartmentDto, Department>()
                .ForMember(x => x.Id, opts => opts.Ignore());
        }
    }
}
