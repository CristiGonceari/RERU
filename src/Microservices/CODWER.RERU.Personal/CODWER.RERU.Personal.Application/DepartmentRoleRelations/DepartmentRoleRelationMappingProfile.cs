using AutoMapper;
using CODWER.RERU.Personal.Data.Entities.OrganizationRoleRelations;
using CODWER.RERU.Personal.DataTransferObjects.DepartmentRoleRelations.Get;

namespace CODWER.RERU.Personal.Application.DepartmentRoleRelations
{
    public class DepartmentRoleRelationMappingProfile : Profile
    {
        public DepartmentRoleRelationMappingProfile()
        {

            CreateMap<ParentDepartmentChildDepartment, DepartmentRelationDto>()
                .ForMember(x => x.Id, opts => opts.MapFrom(op => op.ChildDepartmentId))
                .ForMember(x => x.Name, opts => opts.MapFrom(op => op.ChildDepartment.Name))
                .ForMember(x => x.RelationId, opts => opts.MapFrom(op => op.Id));

            CreateMap<ParentDepartmentChildOrganizationRole, RoleRelationDto>()
                .ForMember(x => x.Id, opts => opts.MapFrom(op => op.ChildOrganizationRoleId))
                .ForMember(x => x.Name, opts => opts.MapFrom(op => op.ChildOrganizationRole.Name))
                .ForMember(x => x.RelationId, opts => opts.MapFrom(op => op.Id));


            CreateMap<ParentOrganizationRoleChildDepartment, DepartmentRelationDto>()
                .ForMember(x => x.Id, opts => opts.MapFrom(op => op.ChildDepartmentId))
                .ForMember(x => x.Name, opts => opts.MapFrom(op => op.ChildDepartment.Name))
                .ForMember(x => x.RelationId, opts => opts.MapFrom(op => op.Id));

            CreateMap<ParentOrganizationRoleChildOrganizationRole, RoleRelationDto>()
                .ForMember(x => x.Id, opts => opts.MapFrom(op => op.ChildOrganizationRoleId))
                .ForMember(x => x.Name, opts => opts.MapFrom(op => op.ChildOrganizationRole.Name))
                .ForMember(x => x.RelationId, opts => opts.MapFrom(op => op.Id));
        }
    }
}
