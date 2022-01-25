using AutoMapper;
using CODWER.RERU.Personal.Data.Entities;
using CODWER.RERU.Personal.DataTransferObjects.OrganizationRoles;
using CVU.ERP.Common.DataTransferObjects.SelectValues;

namespace CODWER.RERU.Personal.Application.OrganizationRoles
{
    public class OrganizationRoleMappingProfile: Profile
    {
        public OrganizationRoleMappingProfile()
        {
            CreateMap<AddEditOrganizationRoleDto, OrganizationRole>()
                .ForMember(x => x.Id, opts => opts.Ignore());

            CreateMap<OrganizationRole, OrganizationRoleDto>();

            CreateMap<OrganizationRole, SelectItem>()
                .ForMember(x => x.Value, opts => opts.MapFrom(op => op.Id.ToString()))
                .ForMember(x => x.Label, opts => opts.MapFrom(op => op.Name));
        }
    }
}
