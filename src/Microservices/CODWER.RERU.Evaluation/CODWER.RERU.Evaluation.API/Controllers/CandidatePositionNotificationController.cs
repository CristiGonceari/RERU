using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.Application.CandidatePositionNotifications.GetCandidatePositionNotifications;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatePositionNotificationController : BaseController
    {
        [HttpGet("{id}")]
        public async Task<List<int>> GetCandidatePositionNotifications([FromRoute] int id)
        {
            var query = new GetCandidatePositionNotificationsQuery { CandidatePositionId = id };
            return await Mediator.Send(query);
        }
    }
}
