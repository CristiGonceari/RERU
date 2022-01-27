using CVU.ERP.Common.Interfaces;
using CVU.ERP.Notifications.Enums;
using System.Threading.Tasks;
using CVU.ERP.Notifications.Email;

namespace CVU.ERP.Notifications.Services.Implementations
{
    public class NotificationService : INotificationService
    {
        private readonly IEmailService _emailService;

        public NotificationService(IEmailService emailService)
        {
            _emailService = emailService;
        }
        
        public async Task<IEmailService> Notify(EmailData data, NotificationType type)
        {
            var result = _emailService;

            if (type == NotificationType.MNotifyNotification)
            {
                // to do
            }
            else if (type == NotificationType.LocalNotification)
            {
                result = await _emailService.QuickSendAsync(data.subject, data.body, data.from, data.to);
            }
            else if (type == NotificationType.Both)
            {
                result = await _emailService.QuickSendAsync(data.subject, data.body, data.from, data.to);
                //+ MNotification todo Service
            }

            return result;
        }
    }
}
