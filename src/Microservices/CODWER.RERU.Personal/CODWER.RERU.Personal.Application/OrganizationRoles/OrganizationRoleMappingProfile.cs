using AutoMapper;
using CODWER.RERU.Personal.DataTransferObjects.OrganizationRoles;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using RERU.Data.Entities.PersonalEntities;

namespace CODWER.RERU.Personal.Application.OrganizationRoles
{
    public class RoleMappingProfile: Profile
    {
        public RoleMappingProfile()
        {
            CreateMap<AddEditRoleDto, Role>()
                .ForMember(x => x.Id, opts => opts.Ignore());

            CreateMap<Role, RoleDto>();

            CreateMap<Role, SelectItem>()
                .ForMember(x => x.Value, opts => opts.MapFrom(op => op.Id.ToString()))
                .ForMember(x => x.Label, opts => opts.MapFrom(op => op.Name));
        }
    }
}
