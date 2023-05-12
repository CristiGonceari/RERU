using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Age.Integrations.MNotify.Models;
using CVU.ERP.Common.Interfaces;
using CVU.ERP.Notifications.Enums;

namespace CVU.ERP.Notifications.Services.Implementations
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly IEmailService _emailService;
        private readonly IMNotifyClient _mNotifyClient;

        public EmailSenderService(IEmailService emailService, IMNotifyClient mNotifyClient)
        {
            _emailService = emailService;
            _mNotifyClient = mNotifyClient;
        }


        public async Task Notify(EmailData data, NotificationType type)
        {

            switch (type)
            {
                case NotificationType.MNotifyNotification:
                    await SendMNotifyEmail(data);
                    break;
                case NotificationType.LocalNotification:
                    await _emailService.QuickSendAsync(data.subject, data.body, data.from, data.to);
                    break;
                case NotificationType.Both:
                    await _emailService.QuickSendAsync(data.subject, data.body, data.from, data.to);
                    await SendMNotifyEmail(data);
                    break;
            }
        }

        public async Task BulkNotify(List<EmailData> data, NotificationType type = NotificationType.Both)
        {

            switch (type)
            {
                case NotificationType.MNotifyNotification:
                {
                    await BulkMnotify(data);
                    break;
                }
                case NotificationType.LocalNotification:
                    await _emailService.BulkSendAsync(data);
                    break;
                case NotificationType.Both:
                {
                    await _emailService.BulkSendAsync(data);
                    await BulkMnotify(data);
                    break;
                }
            }
        }

        private async Task BulkMnotify(List<EmailData> emails)
        {
            foreach (var email in emails)
            {
                await SendMNotifyEmail(email);
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
                Subject = new NotificationContent { Romanian = data.subject },

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
