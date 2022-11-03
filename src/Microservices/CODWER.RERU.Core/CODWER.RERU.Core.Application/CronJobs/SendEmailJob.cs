﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using CVU.ERP.Common.DataTransferObjects.Config;
using CVU.ERP.Common.Interfaces;
using CVU.ERP.Notifications.Enums;
using CVU.ERP.Notifications.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.CronJobs
{
    public class SendEmailJob
    {
        private readonly AppDbContext _appDbContext;
        private readonly INotificationService _notificationService;
        private readonly PlatformConfig _platformConfig;

        public SendEmailJob(AppDbContext appDbContext, INotificationService notificationService, IOptions<PlatformConfig> conf)
        {
            _appDbContext = appDbContext.NewInstance();
            _notificationService = notificationService;
            _platformConfig = conf.Value;
        }

        //public async Task SendEmailNotification1()
        //{
        //    while (_appDbContext.EmailNotifications.Any(en => en.IsSend == false && en.InUpdateProcess == false))
        //    {
        //        var email = await _appDbContext.EmailNotifications
        //            .Include(en => en.Properties)
        //            .FirstOrDefaultAsync(en => en.IsSend == false && en.InUpdateProcess == false);

        //        if (email == null) return;

        //        email.InUpdateProcess = true;
        //        await _appDbContext.SaveChangesAsync();

        //        var template = await GetFileContent(email.HtmlTemplateAddress);

        //        template = email.Properties
        //            .Aggregate(template, (current, property) => current
        //                .Replace(property.KeyToReplace, property.ValueToReplace));

        //        await SendEmail(email, template);

        //        await _appDbContext.SaveChangesAsync();
        //    }
        //}

        public async Task SendEmailNotification()
        {
            while (_appDbContext.EmailNotifications.Any(en => en.IsSend == false && en.InUpdateProcess == false))
            {
                var emailsToSend = new List<EmailData>();

                var emails = _appDbContext.EmailNotifications
                    .Include(en => en.Properties)
                    .Where(en => en.IsSend == false && en.InUpdateProcess == false)
                    .Take(30)  // 30 per minute
                    .ToList();

                Log($"START Email CronJob for {emails.Count} items");

                await SetEmailsInUpdateProcess(emails);

                foreach (var email in emails)
                {
                    if (!string.IsNullOrEmpty(email.To))
                    {
                        var emailData = await GetEmailObject(email);

                        emailsToSend.Add(emailData);

                        email.Status = "Sent";
                    }
                    else
                    {
                        email.Status = "Email recipient is empty";
                    }
                }

                try
                {
                    await _notificationService.BulkNotify(emailsToSend, NotificationType.Both);

                    await SetEmailsStatus(emails, "Sent");
                }
                catch(Exception e)
                {
                    await SetEmailsStatus(emails, $"Error : {e.Message}");
                }
                finally
                {
                    await UpdateEmailRecordsAndRemoveProperties(emails);
                }

                await _appDbContext.SaveChangesAsync();

                Log("END EMAIL CronJob");
            }
        }

        private async Task SetEmailsInUpdateProcess(List<EmailNotification> emails)
        {
            foreach (var email in emails)
            {
                email.InUpdateProcess = true;
            }

            await _appDbContext.SaveChangesAsync();
        }

        private async Task SetEmailsStatus(List<EmailNotification> emails, string message)
        {
            foreach (var email in emails)
            {
                email.Status = message;
            }

            await _appDbContext.SaveChangesAsync();
        }

        private async Task UpdateEmailRecordsAndRemoveProperties(List<EmailNotification> emails)
        {
            var properties = emails.SelectMany(x => x.Properties).ToList();
            _appDbContext.EmailNotificationProperties.RemoveRange(properties);

            foreach (var email in emails)
            {
                email.InUpdateProcess = false;
                email.IsSend = false;
            }

            await _appDbContext.SaveChangesAsync();
        }

        private async Task<EmailData> GetEmailObject(EmailNotification email)
        {
            var template = await GetFileContent(email.HtmlTemplateAddress);

            template = email.Properties
                .Aggregate(template, (current, property) => current.Replace(property.KeyToReplace, property.ValueToReplace));

            return new EmailData
            {
                subject = email.Subject,
                body = $"{template} {GetEmailBodyFooter()}",
                from = "Do Not Reply",
                to = email.To
            };
        }

        //private async Task<EmailData> GetEmail(EmailNotification emailNotification, string template)
        //{
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(emailNotification.To))
        //        {
        //            var emailData = new EmailData
        //            {
        //                subject = emailNotification.Subject,
        //                body = $"{template} {GetEmailBodyFooter()}",
        //                from = "Do Not Reply",
        //                to = emailNotification.To
        //            };

        //            await _notificationService.Notify(emailData, (NotificationType)emailNotification.Type);

        //            emailNotification.Status = "Sent";
        //        }
        //        else
        //        {
        //            emailNotification.Status = "Email recipient is empty";
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        emailNotification.Status = $"Error : {e.Message}";
        //    }
        //    finally
        //    {
        //        _appDbContext.EmailNotificationProperties.RemoveRange(emailNotification.Properties);

        //        emailNotification.InUpdateProcess = false;
        //        emailNotification.IsSend = true;
        //    }
        //}

        private async Task<string> GetFileContent(string path)
            => await File.ReadAllTextAsync(new FileInfo(path).FullName);

        private string GetEmailBodyFooter() =>
            @$"<p style=""font-size: 22px;font-weight: 300;"">Link aplicație: </p><p style=""font-size: 22px;font-weight: 300;"">{_platformConfig.BaseUrl}</p>";

        private void Log(string msg) => Console.WriteLine(msg);
    }
}
