using AutoMapper;
using CODWER.RERU.Core.DataTransferObjects.Roles;
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
        }
    }
}
