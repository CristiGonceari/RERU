using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.Application.InternalNotifications.GetMyNotifications;
using CODWER.RERU.Evaluation.Application.InternalNotifications.MarkNotificationAsSeen;
using CODWER.RERU.Evaluation.DataTransferObjects.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.Tests.Internal.GetTestIdForFastStart;
using CODWER.RERU.Evaluation.DataTransferObjects.InternalTest;
using CVU.ERP.Common.DataTransferObjects.TestDatas;

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

        [HttpGet("test-notification")]
        public async Task<List<GetTestForFastStartDto>> GetUserTestId()
        {
            return await Mediator.Send(new GetTestIdForFastStartQuery());
        }
    }
}
