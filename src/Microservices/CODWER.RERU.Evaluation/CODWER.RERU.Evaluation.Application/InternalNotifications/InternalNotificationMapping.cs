using AutoMapper;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.DataTransferObjects.Notifications;

namespace CODWER.RERU.Evaluation.Application.InternalNotifications
{
    class InternalNotificationMapping : Profile
    {
        public InternalNotificationMapping()
        {
            CreateMap<Notification, NotificationDto>();
        }
    }
}
