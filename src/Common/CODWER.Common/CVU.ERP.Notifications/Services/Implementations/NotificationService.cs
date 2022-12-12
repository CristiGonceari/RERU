using System;
using System.Linq;
using CVU.ERP.Common.Interfaces;
using CVU.ERP.Notifications.Enums;
using System.Threading.Tasks;
using CVU.ERP.Notifications.Email;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using CVU.ERP.Common;

namespace CVU.ERP.Notifications.Services.Implementations
{
    public class NotificationService : INotificationService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IDateTime _dateTime;

        public NotificationService(AppDbContext appDbContext, IDateTime dateTime)
        {
            _appDbContext = appDbContext;
            _dateTime = dateTime;
        }

        public async Task PutEmailInQueue(QueuedEmailData email, NotificationType type = NotificationType.Both)
        {
            await using (var db = _appDbContext.NewInstance())
            {
                var item = new EmailNotification
                {
                    Subject = email.Subject,
                    To = email.To,
                    IsSend = false,
                    InUpdateProcess = false,
                    HtmlTemplateAddress = email.HtmlTemplateAddress,
                    Type = (byte)type,
                    Created = _dateTime.Now,

                    Properties = email.ReplacedValues.Select(x => new EmailNotificationProperty
                    {
                        KeyToReplace = x.Key,
                        ValueToReplace = x.Value
                    }).ToList()
                };

                await db.EmailNotifications.AddAsync(item);
                await db.SaveChangesAsync();
            }
        }
    }
}
