using AutoMapper;
using CODWER.RERU.Core.DataTransferObjects.Modules;
using CODWER.RERU.Core.DataTransferObjects.Users;
using CVU.ERP.Module.Application.Models;

namespace CODWER.RERU.Core.Application.Module {
    public class ModuleMappingProfile : Profile 
    {
        public ModuleMappingProfile ()
        {
            CreateMap<ApplicationUser, ApplicationUserDto>()
                .ForMember(x => x.Name, opts => opts.MapFrom(op => $"{op.FirstName} {op.LastName}"));

            CreateMap<ApplicationUserModule, ApplicationUserModuleDto> ();
            CreateMap<ApplicationModule, ApplicationModuleDto> ();
        }
    }
}