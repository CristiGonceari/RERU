using AutoMapper;
using CODWER.RERU.Personal.DataTransferObjects.ModernLanguageLevel;
using RERU.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.ModernLanguageLevels
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
