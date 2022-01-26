using System.Threading.Tasks;
using CODWER.RERU.Personal.API.Config;
using CODWER.RERU.Personal.Application.OrganizationalCharts.AddOrganizationalChart;
using CODWER.RERU.Personal.Application.OrganizationalCharts.GetOrganizationalChart;
using CODWER.RERU.Personal.Application.OrganizationalCharts.GetOrganizationalCharts;
using CODWER.RERU.Personal.Application.OrganizationalCharts.RemoveOrganizationalChart;
using CODWER.RERU.Personal.Application.OrganizationalCharts.UpdateOrganizationalChart;
using CODWER.RERU.Personal.DataTransferObjects.OrganizationalCharts;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CODWER.RERU.Personal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationalChartController : BaseController
    {
        [HttpGet]
        public async Task<PaginatedModel<OrganizationalChartDto>> GetOrganizationalCharts([FromQuery] GetOrganizationalChartsQuery query)
        {
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpGet("{id}")]
        public async Task<OrganizationalChartDto> GetOrganizationalChart([FromRoute] int id)
        {
            var query = new GetOrganizationalChartQuery { Id = id };
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpPost]
        public async Task<int> CreateOrganizationalChart([FromBody] AddOrganizationalChartCommand command)
        {
            var result = await Mediator.Send(command);

            return result;
        }

        [HttpPatch]
        public async Task<Unit> UpdateOrganizationalChart([FromBody] UpdateOrganizationalChartCommand command)
        {
            var result = await Mediator.Send(command);

            return result;
        }

        [HttpDelete("{id}")]
        public async Task<Unit> RemoveOrganizationalChart([FromRoute] int id)
        {
            var command = new RemoveOrganizationalChartCommand { Id = id };
            var result = await Mediator.Send(command);

            return result;
        }
    }
}
