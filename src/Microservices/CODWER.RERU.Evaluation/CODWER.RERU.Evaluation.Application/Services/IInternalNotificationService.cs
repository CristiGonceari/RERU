using CODWER.RERU.Evaluation.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.Services
{
    public interface IInternalNotificationService
    {
        Task AddNotification(int userProfileId, string notificationCode);
        Task<List<Notification>> GetMyNotifications(int myUserProfileId);
        Task MarkNotificationAsReed(int notificationId);
    }
}
