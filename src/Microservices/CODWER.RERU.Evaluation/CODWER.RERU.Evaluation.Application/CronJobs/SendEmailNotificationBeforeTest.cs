using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Notifications.Email;
using CVU.ERP.Notifications.Enums;
using CVU.ERP.Notifications.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.Application.CronJobs
{
    public class SendEmailNotificationBeforeTest
    {
        private readonly AppDbContext _appDbContext;
        private readonly INotificationService _notificationService;
        private readonly DateTime _timeRangeBeforeStart;
        private readonly DateTime _timeRangeAfterStart;

        public SendEmailNotificationBeforeTest(AppDbContext appDbContext, INotificationService notificationService)
        {
            _appDbContext = appDbContext;
            _notificationService = notificationService;
            _timeRangeBeforeStart = DateTime.Now.AddMinutes(15);
            _timeRangeAfterStart = DateTime.Now.AddMinutes(-1);
        }

        public async Task SendNotificationBeforeTest()
        {
            var tests = _appDbContext.Tests
                .Include(x => x.UserProfile)
                    .Where(test => test.ProgrammedTime <= _timeRangeBeforeStart && 
                                   test.ProgrammedTime >= _timeRangeAfterStart &&
                                   test.TestStatus == TestStatusEnum.Programmed || test.TestStatus == TestStatusEnum.AlowedToStart &&
                                       !test.EmailTestNotifications
                                           .Any(notification => notification.TestId == test.Id && notification.UserProfileId == test.UserProfileId))
                .Select(x => new Test
                {
                    Id = x.Id,
                    UserProfileId = x.UserProfileId,
                    UserProfile = new UserProfile
                    {
                        FirstName = x.UserProfile.FirstName,
                        LastName = x.UserProfile.LastName,
                        Email = x.UserProfile.Email
                    }
                })
                .AsQueryable();

            await SendEmailNotificationToUsers(tests);
        }

        private async Task SendEmailNotificationToUsers(IQueryable<Test> tests)
        {
            var path = new FileInfo("PdfTemplates/EmailNotificationTemplate.html").FullName;

            foreach (var test in tests)
            {
                await GetContentForEmailAndNotify(test, path);

                var emailTestNotification = new EmailTestNotification
                {
                    TestId = test.Id,
                    UserProfileId = test.UserProfileId
                };

               await _appDbContext.EmailTestNotifications.AddAsync(emailTestNotification);
            }

            await _appDbContext.SaveChangesAsync();
        }

        private async Task GetContentForEmailAndNotify(Test test, string path)
        {
            var template = await File.ReadAllTextAsync(path);

            template = template
                .Replace("{user_name}", test.UserProfile.FirstName + " " + test.UserProfile.LastName)
                .Replace("{email_message}", GetTableContent());

            var emailData = new EmailData()
            {
                subject = "Notificare de test",
                body = template,
                from = "Do Not Reply",
                to = test.UserProfile.Email
            };

            await _notificationService.Notify(emailData, NotificationType.Both);
        }

        private string GetTableContent()
        {
            var content = string.Empty;

            content += $@"<p style=""font-size: 22px; font-weight: 300;"">Iti reamintim ca in decurs de 15 minute se va incepe testul la care ai fost asignat, poti accesa linkul: </p>
                            <p style=""font-size: 22px;font-weight: 300;"">http://reru.codwer.com</p>";

            return content;
        }

    }
}
