using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.Application.TestImportProcesses.GetProcessHistory;
using CODWER.RERU.Evaluation.DataTransferObjects.BulkProcesses;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using CVU.ERP.Module.Application.ImportProcesses.StopAllProcesses;
using MediatR;

namespace CODWER.RERU.Evaluation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProcessController : BaseController
    {
        [HttpGet]
        public async Task<List<HistoryProcessDto>> GetBulkProcessHistory()
        {
            var query = new GetBulkProcessHistoryQuery();

            return await Mediator.Send(query);
        }

        [HttpGet("close")]
        public async Task<Unit> CloseAllProcesses()
        {
            var command = new StopAllProcessesCommand();

            return await Mediator.Send(command);
        }
    }
}
