using AutoMapper;
using CODWER.RERU.Personal.DataTransferObjects.DepartmentRoleRelations.Get;
using RERU.Data.Entities.PersonalEntities.OrganizationRoleRelations;

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

            CreateMap<ParentDepartmentChildRole, RoleRelationDto>()
                .ForMember(x => x.Id, opts => opts.MapFrom(op => op.ChildRoleId))
                .ForMember(x => x.Name, opts => opts.MapFrom(op => op.ChildRole.Name))
                .ForMember(x => x.RelationId, opts => opts.MapFrom(op => op.Id));


            CreateMap<ParentRoleChildDepartment, DepartmentRelationDto>()
                .ForMember(x => x.Id, opts => opts.MapFrom(op => op.ChildDepartmentId))
                .ForMember(x => x.Name, opts => opts.MapFrom(op => op.ChildDepartment.Name))
                .ForMember(x => x.RelationId, opts => opts.MapFrom(op => op.Id));

            CreateMap<ParentRoleChildRole, RoleRelationDto>()
                .ForMember(x => x.Id, opts => opts.MapFrom(op => op.ChildRoleId))
                .ForMember(x => x.Name, opts => opts.MapFrom(op => op.ChildRole.Name))
                .ForMember(x => x.RelationId, opts => opts.MapFrom(op => op.Id));
        }
    }
}
