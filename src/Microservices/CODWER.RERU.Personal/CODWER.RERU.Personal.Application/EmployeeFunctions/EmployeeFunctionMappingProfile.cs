using AutoMapper;
using CODWER.RERU.Personal.DataTransferObjects.EmployeeFunctions;
using RERU.Data.Entities;

namespace CODWER.RERU.Personal.Application.EmployeeFunctions
{
    public class EmployeeFunctionMappingProfile : Profile
    {
        public EmployeeFunctionMappingProfile()
        {
            CreateMap<EmployeeFunction, EmployeeFunctionDto>()
                .ForMember(x => x.Type, opts => opts.MapFrom(op => op.Type.ToString()) );
                ;

            CreateMap<EmployeeFunctionDto, EmployeeFunction>()
                .ForMember(x => x.Id, opts => opts.Ignore());
        }
    }
}
