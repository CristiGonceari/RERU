using System.Collections.Generic;
using System.Linq;
using CVU.ERP.Common.Interfaces;
using CVU.ERP.Notifications.Enums;
using System.Threading.Tasks;
using CVU.ERP.Notifications.Email;
using Microsoft.Extensions.Configuration;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CVU.ERP.Notifications.Services.Implementations
{
    public class NotificationService : INotificationService
    {
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public NotificationService(IEmailService emailService, IConfiguration configuration)
        {
            _emailService = emailService;
            _configuration = configuration;
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

        public async Task PutEmailInQueue(QueuedEmailData email, NotificationType type = NotificationType.Both)
        {
            await using (var db = AppDbContext.NewInstance(_configuration))
            {
                var item = new EmailNotification
                {
                    Subject = email.Subject,
                    To = email.To,
                    IsSend = false,
                    InUpdateProcess = false,
                    HtmlTemplateAddress = email.HtmlTemplateAddress,
                    Type = (byte)type,

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
