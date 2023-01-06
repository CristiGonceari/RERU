using CODWER.RERU.Core.Application.Processes.GetProcessHistory;
using CODWER.RERU.Core.DataTransferObjects.Processes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using CVU.ERP.Module.Application.ImportProcesses.StopAllProcesses;

namespace CODWER.RERU.Core.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProcessController : BaseController
    {
        public ProcessController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<List<HistoryProcessesDto>> GetBulkProcessHistory()
        {
            var query = new GetProcessHistoryQuery();

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
