using AutoMapper;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using CODWER.RERU.Core.DataTransferObjects.ModuleRoles;

namespace CODWER.RERU.Core.Application.ModuleRoles
{
    public class ModuleRoleMappingProfile : Profile
    {
        public ModuleRoleMappingProfile()
        {
            CreateMap<Data.Entities.ModuleRole, ModuleRoleRowDto>();
            CreateMap<Data.Entities.ModuleRole, SelectItem>()
                .ForMember(destination => destination.Value, options => options.MapFrom(src=> src.Id.ToString()))
                .ForMember(destination => destination.Label, options => options.MapFrom(src=> src.Name))
                ;

            CreateMap<Data.Entities.ModuleRole, ModuleRoleDto>();

            CreateMap<Data.Entities.ModuleRole, ModuleRoleForSelectDto>();

            CreateMap<AddEditModuleRoleDto, Data.Entities.ModuleRole>();

            CreateMap<ModuleRoleForSelectDto, Data.Entities.ModuleRole>()
                .ForMember(destination => destination.Id, options => options.Ignore());

            CreateMap<Data.Entities.ModuleRole, AddEditModuleRoleDto>()
                .ForMember(destination => destination.Id, options => options.Ignore());

            CreateMap<ModuleRoleRowDto, Data.Entities.ModuleRole>()
                .ForMember(destination => destination.Id, options => options.Ignore());

            CreateMap<ModuleRoleDto, Data.Entities.ModuleRole>()
                .ForMember(destination => destination.Id, options => options.Ignore());
        }
    }
}