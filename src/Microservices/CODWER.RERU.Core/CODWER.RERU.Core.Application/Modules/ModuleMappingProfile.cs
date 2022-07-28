using AutoMapper;
using CODWER.RERU.Core.DataTransferObjects.Modules;
using CVU.ERP.Module.Application.Models;
using CVU.ERP.ServiceProvider.Models;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Core.Application.Modules
{
    public class ModuleMappingProfile : Profile 
    {
        public ModuleMappingProfile() 
        {
            CreateMap<global::RERU.Data.Entities.Module, ModuleDto> ()
                .ForMember (destination => destination.Status, options => options.MapFrom (m => m.Status == ModuleStatus.Online));

            CreateMap<ModuleDto,global::RERU.Data.Entities.Module> ()
                .ForMember (destination => destination.Id, options => options.Ignore ());

            CreateMap<global::RERU.Data.Entities.Module, UserModuleAccessDto> ();

            CreateMap<UserModuleAccessDto, global::RERU.Data.Entities.Module> ()
                .ForMember (destination => destination.Id, options => options.Ignore ());

            CreateMap<global::RERU.Data.Entities.Module, AddEditModuleDto> ();

            CreateMap<AddEditModuleDto, global::RERU.Data.Entities.Module> ()
                .ForMember (destination => destination.Id, options => options.Ignore ())
                .ForMember (destination => destination.Type, options => options.Ignore ())
                .ForMember(destination => destination.Code, options =>
                {
                    options.Condition((src, dest) => dest.Type == ModuleTypeEnum.Dynamic);
                    options.MapFrom(src => src.Code);
                });

            CreateMap<CVU.ERP.Module.Common.Models.ModulePermission, ModulePermission> ();

            CreateMap<global::RERU.Data.Entities.Module, ModuleRolesDto>();

        }
    }
}