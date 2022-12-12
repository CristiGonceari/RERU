using System;
using System.Linq;
using CVU.ERP.Common.Interfaces;
using CVU.ERP.Notifications.Enums;
using System.Threading.Tasks;
using CVU.ERP.Notifications.Email;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Collections.Generic;
using Age.Integrations.MNotify.Models;
using CVU.ERP.Common;

namespace CVU.ERP.Notifications.Services.Implementations
{
    public class NotificationService : INotificationService
    {
        private readonly IEmailService _emailService;
        private readonly AppDbContext _appDbContext;
        private readonly IDateTime _dateTime;
        private readonly IMNotifyClient _mNotifyClient;

        public NotificationService(IEmailService emailService, AppDbContext appDbContext, IDateTime dateTime, IMNotifyClient mNotifyClient)
        {
            _emailService = emailService;
            _appDbContext = appDbContext;
            _dateTime = dateTime;
            _mNotifyClient = mNotifyClient;
        }

        public async Task<IEmailService> Notify(EmailData data, NotificationType type)
        {
            var result = _emailService;

            switch (type)
            {
                case NotificationType.MNotifyNotification:
                    await SendMNotifyEmail(data);
                    break;
                case NotificationType.LocalNotification:
                    result = await _emailService.QuickSendAsync(data.subject, data.body, data.from, data.to);
                    break;
                case NotificationType.Both:
                    result = await _emailService.QuickSendAsync(data.subject, data.body, data.from, data.to);
                    await SendMNotifyEmail(data);
                    break;
            }

            return result;
        }

        public async Task<IEmailService> BulkNotify(List<EmailData> data, NotificationType type = NotificationType.Both)
        {
            var result = _emailService;

            switch (type)
            {
                case NotificationType.MNotifyNotification:
                {
                    await BulkMnotify(data);
                    break;
                }
                case NotificationType.LocalNotification:
                    result = await _emailService.BulkSendAsync(data);
                    break;
                case NotificationType.Both:
                { 
                    result = await _emailService.BulkSendAsync(data);
                    await BulkMnotify(data);
                    break;
                }
            }

            return result;
        }

        private async Task BulkMnotify(List<EmailData> emails)
        {
            foreach (var email in emails)
            {
                await SendMNotifyEmail(email);
            }
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

        private async Task SendMNotifyEmail(EmailData data)
        {
            var notif = new NotificationRequest
            {

                Body = new NotificationContent { Romanian = data.body },
                Recipients = new List<NotificationRecipient>()
                {
                    new NotificationRecipient { Type = NotificationRecipientType.Email, Value = data.to }
                },
                Priority = NotificationPriority.Medium,
                ShortBody = new NotificationContent { Romanian = "Notificare SI RERU al MAI " },
                Subject = new NotificationContent { Romanian = $"{data.subject} - Do Not Reply" },

            };

            try
            {
                await _mNotifyClient.SendNotification(notif);
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERROR Mnotify {e.Message}");
            }
        }
    }
}
