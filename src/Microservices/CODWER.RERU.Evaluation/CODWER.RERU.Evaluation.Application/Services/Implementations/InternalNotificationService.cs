using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.Services.Implementations
{
    public class InternalNotificationService : IInternalNotificationService
    {
        private readonly AppDbContext _appDbContext;

        public InternalNotificationService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task AddNotification(int userProfileId, string notificationCode)
        {
            var notificationToAdd = new Notification()
            {
                UserProfileId = userProfileId,
                MessageCode = notificationCode,
                Seen = false
            };

            await _appDbContext.Notifications.AddAsync(notificationToAdd);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task<List<Notification>> GetMyNotifications(int myUserProfileId)
        {
            return await _appDbContext.Notifications
                .Where(x => x.UserProfileId == myUserProfileId && x.Seen == false)
                .ToListAsync();
        }

        public async Task MarkNotificationAsReed(int notificationId)
        {
            var notification = await _appDbContext.Notifications
                .FirstOrDefaultAsync(x => x.Id == notificationId);

            notification.Seen = true;

            await _appDbContext.SaveChangesAsync();
        }
    }
}
