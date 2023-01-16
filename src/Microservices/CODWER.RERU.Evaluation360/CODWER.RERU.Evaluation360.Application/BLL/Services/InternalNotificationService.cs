using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation360.Application.BLL.Services
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

            using var db = _appDbContext.NewInstance();

            await db.Notifications.AddAsync(notificationToAdd);
            await db.SaveChangesAsync();
        }

        public async Task<List<Notification>> GetMyNotifications(int myUserProfileId)
        {
            using var db = _appDbContext.NewInstance();
            
            return await db.Notifications
                .Where(x => x.UserProfileId == myUserProfileId && x.Seen == false)
                .ToListAsync();
        }

        public async Task MarkNotificationAsReed(int notificationId)
        {
            using var db = _appDbContext.NewInstance();
            var notification = await db.Notifications
                .FirstOrDefaultAsync(x => x.Id == notificationId);

            notification.Seen = true;

            await db.SaveChangesAsync();
        }
    }
}
