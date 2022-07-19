using AutoMapper;
using CODWER.RERU.Personal.DataTransferObjects.VacationConfigurations;
using RERU.Data.Entities.PersonalEntities.Configurations;

namespace CODWER.RERU.Personal.Application.VacationConfigurations
{
    public class VacationConfigurationsMappingProfile : Profile
    {
        public VacationConfigurationsMappingProfile()
        {
            CreateMap<VacationConfigurationDto, VacationConfiguration>()
                .ForMember(x => x.Id, opts => opts.Ignore());

            CreateMap<VacationConfiguration, VacationConfigurationDto>();
        }
    }
}
