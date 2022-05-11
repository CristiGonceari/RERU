using AutoMapper;
using CODWER.RERU.Core.DataTransferObjects.Roles;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using RERU.Data.Entities;

namespace CODWER.RERU.Core.Application.Roles
{
    public class RoleMapping : Profile
    {
        public RoleMapping()
        {
            CreateMap<Role, RoleDto>();

            CreateMap<RoleDto, Role>()
                .ForMember(x => x.Id, opts => opts.Ignore());

            CreateMap<Role, SelectItem>()
                .ForMember(destination => destination.Value, options => options.MapFrom(src => src.ColaboratorId.ToString()))
                .ForMember(destination => destination.Label, options => options.MapFrom(src => src.Name));
        }
    }
}
