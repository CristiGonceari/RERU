using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CODWER.RERU.Core.Application.Users.SaveUserPasswordByEmail;
using CVU.ERP.Common.DataTransferObjects.Config;
using CVU.ERP.Common.Interfaces;
using CVU.ERP.Notifications.Enums;
using CVU.ERP.Notifications.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.CronJobs
{
    public class SendEmailJob
    {
        private readonly AppDbContext _appDbContext;
        private readonly IEmailSenderService _emailSenderService;
        private readonly PlatformConfig _platformConfig;
        private readonly IMediator _mediator;

        public SendEmailJob(AppDbContext appDbContext, IOptions<PlatformConfig> conf, IEmailSenderService emailSenderService, IMediator mediator)
        {
            _appDbContext = appDbContext.NewInstance();
            _emailSenderService = emailSenderService;
            _mediator = mediator;
            _platformConfig = conf.Value;
        }

        public async Task SendEmailNotification()
        {
            while (_appDbContext.EmailNotifications.Any(en => en.IsSend == false && en.InUpdateProcess == false && !string.IsNullOrEmpty(en.To)))
            {
                var emails = _appDbContext.EmailNotifications
                    .Include(en => en.Properties)
                    .Where(en => en.IsSend == false && en.InUpdateProcess == false && !string.IsNullOrEmpty(en.To))
                    .Take(30)  // 30 per minute
                    .ToList();

                foreach (var email in emails)
                {
                    var resetPassword = new SaveUserPasswordByEmailCommand()
                    {
                        Email = email.To
                    };

                    await _mediator.Send(resetPassword);
                }

                Log($"START Email CronJob for {emails.Count} items");

                await SetEmailsInUpdateProcess(emails);

                var emailsToSend = await MapEmails(emails);

                try
                {
                    await _emailSenderService.BulkNotify(emailsToSend, NotificationType.MNotifyNotification);

                    await SetEmailsStatus(emails, "Sent");
                }
                catch(Exception e)
                {
                    await SetEmailsStatus(emails, $"Error : {e.Message}");
                }
                finally
                {
                    await UpdateEmailsAndRemoveProperties(emails);
                }

                await _appDbContext.SaveChangesAsync();

                Log("END EMAIL CronJob");
            }
        }

        private async Task<List<EmailData>> MapEmails(List<EmailNotification> emails)
        {
            var emailsToSend = new List<EmailData>();

            foreach (var email in emails)
            {
                var emailData = await GetEmailObject(email);

                emailsToSend.Add(emailData);
            }

            return emailsToSend;
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

        private async Task UpdateEmailsAndRemoveProperties(List<EmailNotification> emails)
        {
            var properties = emails.SelectMany(x => x.Properties).ToList();
            _appDbContext.EmailNotificationProperties.RemoveRange(properties);

            foreach (var email in emails)
            {
                email.InUpdateProcess = false;
                email.IsSend = true;
            }

            await _appDbContext.SaveChangesAsync();
        }

        private async Task<EmailData> GetEmailObject(EmailNotification email)
        {
            var userPassword = (await _appDbContext.UserProfiles.FirstOrDefaultAsync(x => x.Email == email.To))?.Password;

            var template = await GetFileContent(email.HtmlTemplateAddress);

            template = email.Properties
                .Aggregate(template, (current, property) => current.Replace(property.KeyToReplace, property.ValueToReplace));

            return new EmailData
            {
                subject = email.Subject,
                body = $"{template} {GetEmailBodyFooter(email.To, userPassword)}",
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

        private string GetEmailBodyFooter(string email, string password) =>
             @$"<p><span style=""font-size: 16px;font-weight: 300;"">Link aplicație: </span><span style=""font-size: 16px;font-weight: 300;"">{_platformConfig.BaseUrl}</span></p>
                <p><span style=""font-size: 16px;font-weight: 300;"">Login: </span><span style=""font-size: 16px;font-weight: 300;"">{email}</span></p>
                <p><span style=""font-size: 16px;font-weight: 300;"">Parola: </span><span style=""font-size: 16px;font-weight: 300; color: red;"">{password}</span></p> ";

        private void Log(string msg) => Console.WriteLine(msg);
    }
}
