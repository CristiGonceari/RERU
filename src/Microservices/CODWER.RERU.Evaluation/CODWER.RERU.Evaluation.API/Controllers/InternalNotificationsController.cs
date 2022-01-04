using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.Application.InternalNotifications.GetMyNotifications;
using CODWER.RERU.Evaluation.Application.InternalNotifications.MarkNotificationAsSeen;
using CODWER.RERU.Evaluation.DataTransferObjects.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InternalNotificationsController : BaseController
    {
        [HttpGet("my")]
        public async Task<List<NotificationDto>> GetMyNotifications()
        {
            return await Mediator.Send(new GetMyNotificationsQuery());
        }

        [HttpPost("{id}/seen")]
        public async Task<Unit> MarkNotificationAsSeen([FromRoute] int id)
        {
            return await Mediator.Send(new MarkNotificationAsSeenCommand { NotificationId = id });
        }
    }
}
