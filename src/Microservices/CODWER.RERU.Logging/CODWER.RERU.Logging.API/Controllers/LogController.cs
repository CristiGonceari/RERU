using CODWER.RERU.Logging.API.Config;
using CODWER.RERU.Logging.Application.DeleteLoggingValues;
using CODWER.RERU.Logging.Application.GetLoggingValuesQuery;
using CODWER.RERU.Logging.Application.GetSelectValues.GetEventSelectValues;
using CODWER.RERU.Logging.Application.GetSelectValues.GetProjectSelectValues;
using CODWER.RERU.Logging.DataTransferObjects;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CODWER.RERU.Logging.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : BaseController
    {
        [HttpGet]
        public async Task<PaginatedModel<LogDto>> GetContractors([FromQuery] GetLoggingValuesQuery query)
        {
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpGet("event-select-values")]
        public async Task<List<string>> GetEventSelectValues([FromQuery] GetEventSelectValuesQuery query)
        {
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpGet("project-select-values")]
        public async Task<List<string>> GetProjectSelectValues([FromQuery] GetProjectSelectValuesQuery query)
        {
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpDelete("{years}")]
        public async Task<Unit> DeletePlan([FromRoute] int years)
        {
            return await Mediator.Send(new DeleteLoggingValuesCommand { PeriodOfYears = years });
        }
    }
}
