using CODWER.RERU.Personal.DataTransferObjects.VacationConfigurations;
using MediatR;

namespace CODWER.RERU.Personal.Application.VacationConfigurations.AddUpdateVacationConfigurations
{
    public class AddUpdateVacationConfigurationsCommand : IRequest<Unit>
    {
        public VacationConfigurationDto Data { get; set; }
    }
}
