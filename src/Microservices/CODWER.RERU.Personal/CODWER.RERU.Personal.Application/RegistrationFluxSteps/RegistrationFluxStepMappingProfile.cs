using AutoMapper;
using CODWER.RERU.Personal.DataTransferObjects.RegistrationFluxSteps;
using RERU.Data.Entities;

namespace CODWER.RERU.Personal.Application.RegistrationFluxSteps
{
    public class RegistrationFluxStepMappingProfile : Profile
    {
        public RegistrationFluxStepMappingProfile()
        {
            CreateMap<RegistrationFluxStepDto, RegistrationFluxStep>()
                .ForMember(x => x.Id, opts => opts.Ignore());

            CreateMap<RegistrationFluxStep, RegistrationFluxStepDto>();
        }
    }
}
