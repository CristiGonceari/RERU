using System.Collections.Generic;
using AutoMapper;
using RERU.Data.Entities.PersonalEntities;
using CODWER.RERU.Personal.DataTransferObjects.DepartmentRoleContents;
using CVU.ERP.Common.DataTransferObjects.SelectValues;

namespace CODWER.RERU.Personal.Application.DepartmentRoleContents
{
    public class DepartmentRoleContentMappingProfile : Profile
    {
        public DepartmentRoleContentMappingProfile()
        {
            CreateMap<AddEditDepartmentRoleContentDto, DepartmentRoleContent>();

            #region Department to DepartmentContent Template
            CreateMap<Department, DepartmentRoleContentDto>()
                .ForMember(x => x.DepartmentId, opts => opts.MapFrom(op => op.Id))
                .ForMember(x => x.DepartmentName, opts => opts.MapFrom(op => op.Name))
                .ForMember(x => x.Roles, opts => opts.MapFrom(op => op.DepartmentRoleContents));

            CreateMap<DepartmentRoleContent, RoleFromDepartment>()
                .ForMember(x => x.OrganizationRoleId, opts => opts.MapFrom(x => x.RoleId))
                .ForMember(x => x.OrganizationRoleName, opts => opts.MapFrom(x => x.Role.Name))
                .ForMember(x => x.OrganizationRoleCount, opts => opts.MapFrom(x => x.RoleCount))
                .ForMember(x => x.DepartmentRoleContentId, opts => opts.MapFrom(x => x.Id))
                .ForMember(x => x.ContractorIds, opts => opts.MapFrom(x => new List<SelectItem>()))
                ;
            #endregion
        }
    }
}
