using AutoMapper;
using CODWER.RERU.Core.DataTransferObjects.ModernLanguageLevel;
using RERU.Data.Entities;

namespace CODWER.RERU.Core.Application.ModernLanguageLevels
{
    public class ModernLanguageLevelMappingProfile : Profile
    {
        public ModernLanguageLevelMappingProfile()
        {
            CreateMap<ModernLanguageLevelDto, ModernLanguageLevel>()
                .ForMember(x => x.Id, opts => opts.Ignore());

            CreateMap<ModernLanguageLevel, ModernLanguageLevelDto>();

            CreateMap<AddEditModernLanguageLevelDto, ModernLanguageLevel>()
               .ForMember(x => x.Id, opts => opts.Ignore());

            CreateMap<ModernLanguageLevel, AddEditModernLanguageLevelDto>();
        }
    }
}
