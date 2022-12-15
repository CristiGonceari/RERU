using System.Threading.Tasks;
using CVU.ERP.Notifications.Email;
using CVU.ERP.Notifications.Enums;

namespace CVU.ERP.Notifications.Services
{
    public interface INotificationService
    {
        Task PutEmailInQueue(QueuedEmailData email, NotificationType type= NotificationType.Both);
    }
}
