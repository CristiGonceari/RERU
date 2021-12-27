using System.Threading.Tasks;
using CVU.ERP.Common.Interfaces;
using CVU.ERP.Notifications.Enums;

namespace CVU.ERP.Notifications.Services
{
    public interface INotificationService
    {
        Task<IEmailService> Notify(string subject, string body, string from, string to, NotificationType type);
    }
}
