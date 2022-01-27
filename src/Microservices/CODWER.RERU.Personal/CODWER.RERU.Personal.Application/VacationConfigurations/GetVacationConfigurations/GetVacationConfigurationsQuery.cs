using CODWER.RERU.Personal.DataTransferObjects.VacationConfigurations;
using MediatR;

namespace CODWER.RERU.Personal.Application.VacationConfigurations.GetVacationConfigurations
{
    public class GetVacationConfigurationsQuery : IRequest<VacationConfigurationDto>
    {
    }
}
