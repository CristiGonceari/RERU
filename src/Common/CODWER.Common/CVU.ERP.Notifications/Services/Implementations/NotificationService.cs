using System;
using System.Linq;
using CVU.ERP.Common.Interfaces;
using CVU.ERP.Notifications.Enums;
using System.Threading.Tasks;
using CVU.ERP.Notifications.Email;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Collections.Generic;

namespace CVU.ERP.Notifications.Services.Implementations
{
    public class NotificationService : INotificationService
    {
        private readonly IEmailService _emailService;
        private readonly AppDbContext _appDbContext;

        public NotificationService(IEmailService emailService, AppDbContext appDbContext)
        {
            _emailService = emailService;
            _appDbContext = appDbContext;
        }

        public async Task<IEmailService> Notify(EmailData data, NotificationType type)
        {
            var result = _emailService;

            if (type == NotificationType.MNotifyNotification)
            {
                // to do
            }
            else if (type == NotificationType.LocalNotification)
            {
                result = await _emailService.QuickSendAsync(data.subject, data.body, data.from, data.to);
            }
            else if (type == NotificationType.Both)
            {
                result = await _emailService.QuickSendAsync(data.subject, data.body, data.from, data.to);
                //+ MNotification todo Service
            }

            return result;
        }

        public async Task<IEmailService> BulkNotify(List<EmailData> data, NotificationType type = NotificationType.Both)
        {
            var result = _emailService;

            if (type == NotificationType.MNotifyNotification)
            {
                // to do
            }
            else if (type == NotificationType.LocalNotification)
            {
                result = await _emailService.BulkSendAsync(data);
            }
            else if (type == NotificationType.Both)
            {
                result = await _emailService.BulkSendAsync(data);
                //+ MNotification todo Service
            }

            return result;
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
                    Created = DateTime.Now,

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
