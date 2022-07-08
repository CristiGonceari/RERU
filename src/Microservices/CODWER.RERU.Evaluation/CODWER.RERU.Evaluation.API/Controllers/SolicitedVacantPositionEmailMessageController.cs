using System.Collections.Generic;
using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.Application.SolicitedVacantPositionEmailMessages.GetSolicitedVacantPositionEmailMessage;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.SolicitedVacantPositionEmailMessages.SendSolicitedVacantPositionEmailMessages;

namespace CODWER.RERU.Evaluation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SolicitedVacantPositionEmailMessageController : BaseController
    {
        [HttpGet]
        public async Task<string> GetSolicitedTests([FromQuery] GetSolicitedVacantPositionEmailMessageQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<int> ChangeStatus([FromBody] SendSolicitedVacantPositionEmailMessagesCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
