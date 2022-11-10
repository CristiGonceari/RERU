using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.Application.CandidatePositionNotifications.GetCandidatePositionNotifications;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.CandidatePositionNotifications.GetCurrentUserId;
using CODWER.RERU.Evaluation.Application.CandidatePositionNotifications.GetNotificatedUsers;
using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;

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

        [HttpGet("users/{id}")]
        public async Task<List<UserProfileDto>> GtNotificatedUsers([FromRoute] int id)
        {
            var query = new GetNotificatedUsersQuery() { CandidatePositionId = id };
            return await Mediator.Send(query);
        }

        [HttpGet]
        public async Task<int> GetCurrentUserId()
        {
            var query = new GetCurrentUserIdQuery() ;
            return await Mediator.Send(query);
        }
    }
}
