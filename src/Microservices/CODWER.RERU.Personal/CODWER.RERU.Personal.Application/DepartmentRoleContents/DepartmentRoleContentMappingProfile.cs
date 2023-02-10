using System.Collections.Generic;
using AutoMapper;
using RERU.Data.Entities.PersonalEntities;
using CODWER.RERU.Personal.DataTransferObjects.DepartmentRoleContents;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using RERU.Data.Entities;

namespace CODWER.RERU.Personal.Application.DepartmentRoleContents
{
    public class DepartmentRoleContentMappingProfile : Profile
    {
        public DepartmentRoleContentMappingProfile()
        {
            CreateMap<AddEditDepartmentRoleContentDto, DepartmentRoleContent>();

            CreateMap<UserProfile, DepartmentRoleUserProfileDto>()
                .ForMember(x => x.DepartmentId, opts => opts.MapFrom(op => op.Department.Id))
                .ForMember(x => x.DepartmentName, opts => opts.MapFrom(op => op.Department.Name))
                .ForMember(x => x.FullName, opts => opts.MapFrom(op => op.FullName))
                .ForMember(x => x.RoleId, opts => opts.MapFrom(op => op.Role.Id))
                .ForMember(x => x.RoleName, opts => opts.MapFrom(op => op.Role.Name))
                .ForMember(x => x.FunctionId, opts => opts.MapFrom(op => op.EmployeeFunction.Id))
                .ForMember(x => x.FunctionName, opts => opts.MapFrom(op => op.EmployeeFunction.Name))
                ;

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
