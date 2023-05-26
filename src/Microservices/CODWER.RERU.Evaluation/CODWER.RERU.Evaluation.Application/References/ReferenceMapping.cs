using AutoMapper;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using RERU.Data.Entities;
using RERU.Data.Entities.PersonalEntities;

namespace CODWER.RERU.Evaluation.Application.References
{
    public class ReferenceMapping : Profile
    {
        public ReferenceMapping()
        {
            CreateMap<EmployeeFunction, SelectItem>()
                .ForMember(x => x.Value, opts => opts.MapFrom(sv => sv.ColaboratorId.ToString()))
                .ForMember(x => x.Label, opts => opts.MapFrom(sv => sv.Name));
            CreateMap<Department, SelectItem>()
                .ForMember(x => x.Value, opts => opts.MapFrom(sv => sv.ColaboratorId.ToString()))
                .ForMember(x => x.Label, opts => opts.MapFrom(sv => sv.Name));
            CreateMap<Role, SelectItem>()
                .ForMember(x => x.Value, opts => opts.MapFrom(sv => sv.ColaboratorId.ToString()))
                .ForMember(x => x.Label, opts => opts.MapFrom(sv => sv.Name));
        }
    }
}
