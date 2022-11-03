using System.Collections.Generic;
using System.Threading.Tasks;
using CVU.ERP.Common.Interfaces;
using CVU.ERP.Notifications.Email;
using CVU.ERP.Notifications.Enums;

namespace CVU.ERP.Notifications.Services
{
    public interface INotificationService
    {
        Task<IEmailService> Notify(EmailData data, NotificationType type);
        Task<IEmailService> BulkNotify(List<EmailData> data, NotificationType type = NotificationType.Both);
        Task PutEmailInQueue(QueuedEmailData email, NotificationType type= NotificationType.Both);
    }
}
