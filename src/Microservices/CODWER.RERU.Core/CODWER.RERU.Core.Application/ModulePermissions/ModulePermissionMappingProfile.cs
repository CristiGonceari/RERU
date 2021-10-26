using AutoMapper;
using CODWER.RERU.Core.Data.Entities;
using CODWER.RERU.Core.DataTransferObjects.ModulePermissions;
using CODWER.RERU.Core.DataTransferObjects.ModuleRoles;

namespace CODWER.RERU.Core.Application.ModulePermissions
{
    public class ModulePermissionMappingProfile : Profile
    {
        public ModulePermissionMappingProfile()
        {
            CreateMap<ModulePermission, ModulePermissionRowDto>();

            CreateMap<ModulePermissionRowDto, ModulePermission>()
                .ForMember(destination => destination.Id, options => options.Ignore());

            CreateMap<ModulePermission, ModuleRolePermissionGrantedRowDto>()
                .ForMember(destination => destination.Description, options => options.MapFrom(x=>x.Description))
                .ForMember(destination => destination.Code, options => options.MapFrom(x => x.Code));

            CreateMap<ModuleRolePermissionGrantedRowDto, ModulePermission>()
                .ForMember(destination => destination.Description, options => options.MapFrom(x => x.Description))
                .ForMember(destination => destination.Code, options => options.MapFrom(x => x.Code))
                .ForMember(destination => destination.Id, options => options.Ignore());
        }
    }
}