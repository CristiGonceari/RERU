using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.Application.TestBulkProcesses;
using CODWER.RERU.Evaluation.DataTransferObjects.BulkProcesses;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetBulkProcessHistoryController : BaseController
    {
        [HttpGet]
        public async Task<List<HistoryProcessDto>> GetBulkProcessHistory()
        {
            var query = new GetBulkProcessHistoryQuery();

            return await Mediator.Send(query);
        }
    }
}
