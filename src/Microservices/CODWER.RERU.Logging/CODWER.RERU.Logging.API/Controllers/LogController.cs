using System.Threading.Tasks;
using CODWER.RERU.Logging.API.Config;
using CODWER.RERU.Logging.Application.GetLoggingValuesQuery;
using CODWER.RERU.Logging.DataTransferObjects;
using CVU.ERP.Common.Pagination;
using Microsoft.AspNetCore.Mvc;

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
    }
}
