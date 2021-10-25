using AutoMapper;
using CODWER.RERU.Core.Data.Entities.Enums;
using CODWER.RERU.Core.DataTransferObjects.Modules;
using CVU.ERP.Module.Application.Models;

namespace CODWER.RERU.Core.Application.Modules
{
    public class ModuleMappingProfile : Profile 
    {
        public ModuleMappingProfile() 
        {
            CreateMap<Data.Entities.Module, ModuleDto> ()
                .ForMember (destination => destination.Status, options => options.MapFrom (m => m.Status == Data.Entities.Enums.ModuleStatus.Online));

            CreateMap<ModuleDto, Data.Entities.Module> ()
                .ForMember (destination => destination.Id, options => options.Ignore ());

            CreateMap<Data.Entities.Module, UserModuleAccessDto> ();

            CreateMap<UserModuleAccessDto, Data.Entities.Module> ()
                .ForMember (destination => destination.Id, options => options.Ignore ());

            CreateMap<Data.Entities.Module, AddEditModuleDto> ();

            CreateMap<AddEditModuleDto, Data.Entities.Module> ()
                .ForMember (destination => destination.Id, options => options.Ignore ())
                .ForMember (destination => destination.Type, options => options.Ignore ())
                .ForMember(destination => destination.Code, options =>
                {
                    options.Condition((src, dest) => dest.Type == ModuleTypeEnum.Dynamic);
                    options.MapFrom(src => src.Code);
                });

            CreateMap<CVU.ERP.Module.Common.Models.ModulePermission, Data.Entities.ModulePermission> ();

            CreateMap<Data.Entities.Module, ModuleRolesDto>();

        }
    }
}