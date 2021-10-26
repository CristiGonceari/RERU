using AutoMapper;
using CODWER.RERU.Core.Data.Entities;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using CODWER.RERU.Core.DataTransferObjects.ModuleRoles;

namespace CODWER.RERU.Core.Application.ModuleRoles
{
    public class ModuleRoleMappingProfile : Profile
    {
        public ModuleRoleMappingProfile()
        {
            CreateMap<ModuleRole, ModuleRoleRowDto>();
            CreateMap<ModuleRole, SelectItem>()
                .ForMember(destination => destination.Value, options => options.MapFrom(src=> src.Id.ToString()))
                .ForMember(destination => destination.Label, options => options.MapFrom(src=> src.Name))
                ;

            CreateMap<ModuleRole, ModuleRoleDto>();

            CreateMap<ModuleRole, ModuleRoleForSelectDto>();

            CreateMap<AddEditModuleRoleDto, ModuleRole>();

            CreateMap<ModuleRoleForSelectDto, ModuleRole>()
                .ForMember(destination => destination.Id, options => options.Ignore());

            CreateMap<ModuleRole, AddEditModuleRoleDto>()
                .ForMember(destination => destination.Id, options => options.Ignore());

            CreateMap<ModuleRoleRowDto, ModuleRole>()
                .ForMember(destination => destination.Id, options => options.Ignore());

            CreateMap<ModuleRoleDto, ModuleRole>()
                .ForMember(destination => destination.Id, options => options.Ignore());
        }
    }
}