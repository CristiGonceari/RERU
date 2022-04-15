using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.Notifications;
using RERU.Data.Entities;

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
