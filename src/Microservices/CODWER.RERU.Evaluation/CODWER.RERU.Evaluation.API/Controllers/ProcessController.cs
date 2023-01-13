using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.Application.TestImportProcesses.GetProcessHistory;
using CODWER.RERU.Evaluation.DataTransferObjects.BulkProcesses;
using CVU.ERP.Module.Application.ImportProcessServices;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProcessController : BaseController
    {
        private readonly IImportProcessService _importProcessService;

        public ProcessController(IImportProcessService importProcessService)
        {
            _importProcessService = importProcessService;
        }

        [HttpGet]
        public async Task<List<HistoryProcessDto>> GetBulkProcessHistory()
        {
            var query = new GetBulkProcessHistoryQuery();

            return await Mediator.Send(query);
        }

        [HttpGet("close")]
        public async Task<Unit> CloseAllProcesses()
        {
            return await _importProcessService.StopAllProcesses();
        }
    }
}
