using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CVU.ERP.Notifications.Email;
using CVU.ERP.Notifications.Enums;
using CVU.ERP.Notifications.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.CronJobs
{
    public class SendEmailJob
    {
        private readonly AppDbContext _appDbContext;
        private readonly INotificationService _notificationService;

        public SendEmailJob(AppDbContext appDbContext, INotificationService notificationService)
        {
            _appDbContext = appDbContext.NewInstance();
            _notificationService = notificationService;
        }

        public async Task SendEmailNotification()
        {
            while (_appDbContext.EmailNotifications.Any(en => en.IsSend == false && en.InUpdateProcess == false))
            {
                var email = await _appDbContext.EmailNotifications
                    .Include(en => en.Properties)
                    .FirstOrDefaultAsync(en => en.IsSend == false && en.InUpdateProcess == false);

                if (email == null) return;

                email.InUpdateProcess = true;
                await _appDbContext.SaveChangesAsync();

                var template = await GetFileContent(email.HtmlTemplateAddress);

                template = email.Properties
                    .Aggregate(template, (current, property) => current
                        .Replace(property.KeyToReplace, property.ValueToReplace));

                await SendEmail(email, template);

                await _appDbContext.SaveChangesAsync();
            }
        }

        private async Task SendEmail(EmailNotification emailNotification, string template)
        {
            try
            {
                if (!string.IsNullOrEmpty(emailNotification.To))
                {
                    var emailData = new EmailData
                    {
                        subject = emailNotification.Subject,
                        body = template,
                        from = "Do Not Reply",
                        to = emailNotification.To
                    };

                    await _notificationService.Notify(emailData, (NotificationType)emailNotification.Type);

                    emailNotification.Status = "Sent";
                }
                else
                {
                    emailNotification.Status = "Email recipient is empty";
                }
            }
            catch (Exception e)
            {
                emailNotification.Status = $"Error : {e.Message}";
            }
            finally
            {
                _appDbContext.EmailNotificationProperties.RemoveRange(emailNotification.Properties);

                emailNotification.InUpdateProcess = false;
                emailNotification.IsSend = true;
            }
        }

        private async Task<string> GetFileContent(string path)
            => await File.ReadAllTextAsync(new FileInfo(path).FullName);
    }
}
