//using System;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;
//using CVU.ERP.Notifications.Email;
//using CVU.ERP.Notifications.Enums;
//using CVU.ERP.Notifications.Services;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using RERU.Data.Entities;
//using RERU.Data.Persistence.Context;

//namespace CODWER.RERU.Evaluation.Application.CronJobs
//{
//    public class SendEmailJob
//    {
//        private readonly AppDbContext _appDbContext;
//        private readonly INotificationService _notificationService;

//        public SendEmailJob(IConfiguration configurationService, INotificationService notificationService)
//        {
//            _notificationService = notificationService;
//            _appDbContext = AppDbContext.NewInstance(configurationService);
//        }

//        public async Task SendEmailNotification()
//        {
//            var emails = _appDbContext.EmailNotifications
//                .Include(en => en.Properties)
//                .Where(en => en.IsSend == false && en.InUpdateProcess == false);

//            await emails.ForEachAsync(x => x.InUpdateProcess = true);
//            await _appDbContext.SaveChangesAsync();

//            var emailCount = 0;

//            foreach (var emailNotification in emails)
//            {
//                emailCount++;
//                var template = await GetFileContent(emailNotification.HtmlTemplateAddress);

//                template = emailNotification.Properties
//                    .Aggregate(template, (current, property) => current
//                        .Replace(property.KeyToReplace, property.ValueToReplace));

//                await SendEmail(emailNotification, template);

//                if (emailCount <= 20) continue;
                
//                await _appDbContext.SaveChangesAsync();
//                emailCount=0;
//            }

//            await _appDbContext.SaveChangesAsync();
//        }

//        private async Task SendEmail(EmailNotification emailNotification, string template)
//        {
//            try
//            {
//                var emailData = new EmailData
//                {
//                    subject = emailNotification.Subject,
//                    body = template,
//                    from = "Do Not Reply",
//                    to = emailNotification.To
//                };

//                await _notificationService.Notify(emailData, (NotificationType)emailNotification.Type);

//                _appDbContext.EmailNotifications.Remove(emailNotification);
//            }
//            catch (Exception e)
//            {
//                emailNotification.Status = $"Error : {e.Message}";
//            }
//            finally
//            {
//                emailNotification.InUpdateProcess = false;
//                emailNotification.IsSend = true;
//            }
//        }

//        private async Task<string> GetFileContent(string path)
//            => await File.ReadAllTextAsync(new FileInfo(path).FullName);
//    }
//}
