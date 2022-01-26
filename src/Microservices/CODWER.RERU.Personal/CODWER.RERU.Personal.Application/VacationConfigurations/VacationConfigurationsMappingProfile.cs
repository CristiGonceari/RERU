using AutoMapper;
using CODWER.RERU.Personal.Data.Entities.Configurations;
using CODWER.RERU.Personal.DataTransferObjects.VacationConfigurations;

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
