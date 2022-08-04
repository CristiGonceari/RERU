using AutoMapper;
using CODWER.RERU.Core.DataTransferObjects.RegistrationFluxSteps;
using RERU.Data.Entities;

namespace CODWER.RERU.Core.Application.RegistrationFluxSteps
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
