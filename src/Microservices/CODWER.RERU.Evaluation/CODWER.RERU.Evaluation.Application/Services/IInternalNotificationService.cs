using System.Collections.Generic;
using System.Threading.Tasks;
using RERU.Data.Entities;

namespace CODWER.RERU.Evaluation.Application.Services
{
    public interface IInternalNotificationService
    {
        Task AddNotification(int userProfileId, string notificationCode);
        Task<List<Notification>> GetMyNotifications(int myUserProfileId);
        Task MarkNotificationAsReed(int notificationId);
    }
}
