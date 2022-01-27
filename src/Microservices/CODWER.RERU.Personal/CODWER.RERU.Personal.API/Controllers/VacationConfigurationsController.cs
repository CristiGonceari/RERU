using System.Threading.Tasks;
using CODWER.RERU.Personal.API.Config;
using CODWER.RERU.Personal.Application.VacationConfigurations.AddUpdateVacationConfigurations;
using CODWER.RERU.Personal.Application.VacationConfigurations.GetVacationConfigurations;
using CODWER.RERU.Personal.DataTransferObjects.VacationConfigurations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CODWER.RERU.Personal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacationConfigurationsController : BaseController
    {
        [HttpGet]
        public async Task<VacationConfigurationDto> GetVacationConfigurations()
        {
            var result = await Mediator.Send(new GetVacationConfigurationsQuery());

            return result;
        }

        [HttpPost]
        public async Task<Unit> CreateVacationConfiguration([FromBody] VacationConfigurationDto dto)
        {
            var command = new AddUpdateVacationConfigurationsCommand
            {
                Data = dto
            };

            var result = await Mediator.Send(command);

            return result;
        }
    }
}