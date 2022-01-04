using CODWER.RERU.Evaluation.DataTransferObjects.Notifications;
using MediatR;
using System.Collections.Generic;

namespace CODWER.RERU.Evaluation.Application.InternalNotifications.GetMyNotifications
{
    public class GetMyNotificationsQuery : IRequest<List<NotificationDto>>
    {
    }
}
