using CODWER.RERU.Core.Application.Processes.GetProcessHistory;
using CODWER.RERU.Core.DataTransferObjects.Processes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using CVU.ERP.Module.Application.ImportProcessServices;

namespace CODWER.RERU.Core.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProcessController : BaseController
    {
        private readonly IImportProcessService _importProcessService;

        public ProcessController(IMediator mediator, IImportProcessService importProcessService) : base(mediator)
        {
            _importProcessService = importProcessService;
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
            return await _importProcessService.StopAllProcesses();
        }
    }
}
