﻿using CODWER.RERU.Core.Application.Processes.GetProcessHistory;
using CODWER.RERU.Core.DataTransferObjects.Processes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProcessController : BaseController
    {
        public ProcessController(IMediator mediator) : base(mediator)
        {
        }

        public async Task<List<HistoryProcessesDto>> GetBulkProcessHistory()
        {
            var query = new GetProcessHistoryQuery();

            return await Mediator.Send(query);
        }
    }
}