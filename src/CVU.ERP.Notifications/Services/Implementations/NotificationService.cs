using CVU.ERP.Common.Interfaces;
using CVU.ERP.Notifications.Enums;
using System.Threading.Tasks;

namespace CVU.ERP.Notifications.Services.Implementations
{
    public class NotificationService : INotificationService
    {
        private readonly IEmailService _emailService;

        public NotificationService(IEmailService emailService)
        {
            _emailService = emailService;
        }
        
        public async Task<IEmailService> Notify(string subject, string body, string from, string to, NotificationType type)
        {
            var result = _emailService;

            if (type == NotificationType.MNotifyNotification)
            {
                // to do
            }
            else if (type == NotificationType.LocalNotification)
            {
                result = await _emailService.QuickSendAsync(subject, body, from, to);
            }
            else if (type == NotificationType.Both)
            {
                // to do
            }

            return result;
        }
    }
}
