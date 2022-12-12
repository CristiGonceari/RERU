using System.Collections.Generic;
using System.Threading.Tasks;
using CVU.ERP.Common.Interfaces;
using CVU.ERP.Notifications.Enums;

namespace CVU.ERP.Notifications.Services
{
    public interface IEmailSenderService
    {
        Task Notify(EmailData data, NotificationType type);
        Task BulkNotify(List<EmailData> data, NotificationType type = NotificationType.Both);
    }
}
