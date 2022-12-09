﻿using CVU.ERP.Notifications.Email;
using CVU.ERP.Notifications.Enums;
using CVU.ERP.Notifications.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CVU.ERP.Common;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;
using Microsoft.Extensions.Options;
using CVU.ERP.Common.DataTransferObjects.Config;

namespace CODWER.RERU.Evaluation.Application.CronJobs
{
    public class SendEmailNotificationBeforeTest
    {
        private readonly AppDbContext _appDbContext;
        private readonly INotificationService _notificationService;
        private readonly IDateTime _dateTime;
        private readonly DateTime _timeRangeBeforeStart;
        private readonly DateTime _timeRangeAfterStart;

        public SendEmailNotificationBeforeTest(AppDbContext appDbContext, INotificationService notificationService, IDateTime dateTime)
        {
            _appDbContext = appDbContext;
            _notificationService = notificationService;
            _dateTime = dateTime;
            _timeRangeBeforeStart = _dateTime.Now.AddMinutes(15);
            _timeRangeAfterStart = _dateTime.Now.AddMinutes(-1);
        }

        public async Task SendNotificationBeforeTest()
        {
            var tests = _appDbContext.Tests
                .Include(x => x.UserProfile)
                .Include(x => x.TestTemplate)
                    .Where(test => test.ProgrammedTime <= _timeRangeBeforeStart && 
                                   test.ProgrammedTime >= _timeRangeAfterStart &&
                                   test.TestTemplate.Mode == TestTemplateModeEnum.Test &&
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
            foreach (var test in tests)
            {
                await GetContentForEmailAndNotify(test);

                var emailTestNotification = new EmailTestNotification
                {
                    TestId = test.Id,
                    UserProfileId = test.UserProfileId
                };

               await _appDbContext.EmailTestNotifications.AddAsync(emailTestNotification);
            }

            await _appDbContext.SaveChangesAsync();
        }

        private async Task GetContentForEmailAndNotify(Test test)
        {
            await _notificationService.PutEmailInQueue(new QueuedEmailData
            {
                Subject = "Notificare de test",
                To = test.UserProfile.Email,
                HtmlTemplateAddress = "Templates/Evaluation/EmailNotificationTemplate.html",
                ReplacedValues = new Dictionary<string, string>()
                {
                    { "{user_name}", test.UserProfile.FullName },
                    { "{email_message}", GetEmailContent() }
                }
            });
        }

        private string GetEmailContent()
        => $@"<p style=""font-size: 22px; font-weight: 300;"">vă reamintim ca in decurs de 15 minute se va incepe testul la care ați fost asignat, puteți accesa linkul: </p>";
    }
}
