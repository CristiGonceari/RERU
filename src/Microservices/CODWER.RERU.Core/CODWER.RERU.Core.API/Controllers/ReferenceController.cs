using CODWER.RERU.Core.Application.References.GetUserProcess;
using CVU.ERP.Module.Application.ImportProcesses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReferenceController : BaseController
    {
        public ReferenceController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("processes-value/select-values")]
        public async Task<List<ProcessDataDto>> GetProcesses()
        {
            var query = new GetUserProcessQuery();

            return await Mediator.Send(query);
        }
    }
}
