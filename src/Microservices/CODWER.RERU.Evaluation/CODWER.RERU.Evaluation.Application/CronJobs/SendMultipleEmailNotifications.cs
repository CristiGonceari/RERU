using System;
using System.IO;
using CVU.ERP.Notifications.Services;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.Configuration.Conventions;
using CODWER.RERU.Evaluation.Application.Models;
using CODWER.RERU.Evaluation.Application.Validation;
using CVU.ERP.Notifications.Email;
using CVU.ERP.Notifications.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CODWER.RERU.Evaluation.Application.CronJobs
{
    public class SendMultipleEmailNotifications
    {
        private readonly AppDbContext _appDbContext;
        private readonly INotificationService _notificationService;
        private readonly PlatformConfig _platformConfig;


        public SendMultipleEmailNotifications(AppDbContext appDbContext, 
            INotificationService notificationService,
            IOptions<PlatformConfig> options
            )
        {
            _appDbContext = appDbContext;
            _notificationService = notificationService; 
            _platformConfig = options.Value;
        }

        //public async Task SendEmailNotifications()
        //{
        //    var emailNotifications = _appDbContext.EmailNotifications.Where(x => x.IsSend == false)
        //                                                    .Select(x => new EmailNotification
        //                                                    {
        //                                                        Id = x.Id,
        //                                                        IsSend = x.IsSend,
        //                                                        ItemId = x.ItemId,
        //                                                        EmailType = x.EmailType
        //                                                    }).ToList();

        //    foreach (var emailNotification in emailNotifications)
        //    {
        //        switch (emailNotification.EmailType)
        //        {
        //            //case EmailType.AssignUserToEvent:
        //            //    await AssignUserToEventEmail(emailNotification.ItemId);
        //            //    break;
        //            case EmailType.AssignEvaluatorToEvent:
        //                await AssignEvaluatorToEventEmail(emailNotification.ItemId);
        //                break;
        //            //case EmailType.AssignResponsiblePersonToEvent:
        //            //    await AssignResponsiblePersonToEventEmail(emailNotification.ItemId);
        //            //    break;
        //            case EmailType.AddTestCandidateEmail:
        //                await SendEmailForTestCandidate(emailNotification.ItemId);
        //                break;
        //            case EmailType.AddTestEvaluatorEmail:
        //                await SendEmailForTestEvaluator(emailNotification.ItemId);
        //                break;
        //            default:
        //                throw new ArgumentOutOfRangeException();
        //        }

        //        _appDbContext.EmailNotifications.First(x => x.Id == emailNotification.Id).IsSend = true;
        //    }

        //    await _appDbContext.SaveChangesAsync();
        //}

        //public async Task SendEmailForTestCandidate(int testId)
        //{
        //    var template = await GetFilePath();

        //    var item = await _appDbContext.Tests
        //        .Include(x => x.TestTemplate)
        //        .FirstOrDefaultAsync(x => x.Id == testId);
           
        //    template = template
        //        .Replace("{user_name}", item.UserProfile.FirstName + " " + item.UserProfile.LastName)
        //        .Replace("{email_message}", await GetTableContent(item, true));

        //    await ReplaceAndNotify(template, item.UserProfile.Email);
        //}

        //public async Task SendEmailForTestEvaluator(int testId)
        //{
        //    var template = await GetFilePath();

        //    var item = await _appDbContext.Tests
        //        .Include(x => x.TestTemplate)
        //        .FirstOrDefaultAsync(x => x.Id == testId);

        //    template = template
        //        .Replace("{email_message}", await GetTableContent(item, false))
        //        .Replace("{user_name}", item.Evaluator.FirstName + " " + item.Evaluator.LastName);

        //    await ReplaceAndNotify(template, item.UserProfile.Email);
        //}

        //public async Task AssignUserToEventEmail(int id)
        //{
        //    var item = await _appDbContext.EventUsers
        //        .Include(x => x.UserProfile)
        //        .Include(x => x.Event)
        //        .FirstOrDefaultAsync(x => x.Id == id);

        //    var template = await GetFilePath();

        //    template = template
        //        .Replace("{user_name}", item.UserProfile.FirstName + " " + item.UserProfile.LastName)
        //        .Replace("{email_message}", await GetTableContent(item));

        //    await ReplaceAndNotify(template, item.UserProfile.Email);
        //}

        //public async Task AssignEvaluatorToEventEmail(int id)
        //{
        //    var item = await _appDbContext.EventEvaluators
        //        .Include(eu => eu.Evaluator)
        //        .Include(eu => eu.Event)
        //        .FirstOrDefaultAsync(x => x.Id == id);

        //    var template = await GetFilePath();

        //    template = template
        //        .Replace("{user_name}", item.Evaluator.FirstName + " " + item.Evaluator.LastName)
        //        .Replace("{email_message}", await GetTableContent(item));

        //    await ReplaceAndNotify(template, item.Evaluator.Email);
        //}

        //public async Task AssignResponsiblePersonToEventEmail(int id)
        //{
        //    var item = await _appDbContext.EventResponsiblePersons
        //        .Include(eu => eu.UserProfile)
        //        .Include(eu => eu.Event)
        //        .FirstOrDefaultAsync(x => x.Id == id);

        //    var template = await GetFilePath();

        //    template = template
        //        .Replace("{user_name}", item.UserProfile.FirstName + " " + item.UserProfile.LastName)
        //        .Replace("{email_message}", await GetTableContent(item));

        //    await ReplaceAndNotify(template, item.UserProfile.Email);
        //}

        //private async Task<string> GetTableContent(EventUser eventUser)
        //{
        //    var content = $@"<p style=""font-size: 22px; font-weight: 300;"">Ați fost invitat la evenimentul ""{eventUser.Event.Name}"" în rol de candidat.</p>";

        //    return content;
        //}

        //private async Task<string> GetTableContent(EventEvaluator eventEvaluator)
        //{
        //    var content = $@"<p style=""font-size: 22px; font-weight: 300;"">Ați fost invitat la evenimentul ""{eventEvaluator.Event.Name}"" în rol de evaluator.</p>";

        //    return content;
        //}

        //private async Task<string> GetTableContent(EventResponsiblePerson eventResponsiblePerson)
        //{
        //    var content = $@"<p style=""font-size: 22px; font-weight: 300;"">Ați fost invitat la evenimentul ""{eventResponsiblePerson.Event.Name}"" în rol de persoană responsabilă.</p>";

        //    return content;
        //}

        //private async Task<string> GetTableContent(Test item, bool evaluat)
        //{
        //    var content = string.Empty;

        //    if (evaluat)
        //    {
        //        content += $@"<p style=""font-size: 22px; font-weight: 300;"">Ați fost invitat la testul ""{item.TestTemplate.Name}"" în rol de candidat.</p>
        //                  <p style=""font-size: 22px; font-weight: 300;""> Testul va avea loc pe data: {item.ProgrammedTime.ToString("dd/MM/yyyy")}.</p> 
        //                  <p>Pentru a accesa testul programat pe Dvs, urmati pasii:</p>
        //                  <p>1. Logati- va pe pagina {_platformConfig.BaseUrl}</p>
        //                  <p>2.Click pe butonul ""Evaluare"" </p>
        //                  <p> 3.Click pe butonul ""Activitatile mele"" </p>
        //                  <p> 4.Din meniul din stanga alegeti optiunea ""Eveniment"", daca testul a fost programat cu eveniment </p>
        //                  <p> 5.Din meniul din stanga alegeti optiunea ""Teste"", daca testul a fost programat fara eveniment </p>
        //                  <p> 6.Pentru a incepe testul, click ""Incepe Testul"" din partea dreapta de jos a paginii </p>
        //                  <p> 7.Bifati acceptarea regulilor, selecteaza ""Accepta"", si click butonul ""Incepe"" </p>";
        //    }
        //    else
        //    {
        //        content += $@"<p style=""font-size: 22px; font-weight: 300;"">Ați fost invitat la testul ""{item.TestTemplate.Name}"" în rol de evaluator.</p>";
        //    }

        //    return content;
        //}

        //private async Task ReplaceAndNotify(string template, string email)
        //{
        //    var emailData = new EmailData()
        //    {
        //        subject = "Invitație la eveniment",
        //        body = template,
        //        from = "Do Not Reply",
        //        to = email
        //    };

        //    await _notificationService.Notify(emailData, NotificationType.Both);
        //}

        //private async Task<string> GetFilePath()
        //{
        //    return await File.ReadAllTextAsync(new FileInfo("PdfTemplates/EmailNotificationTemplate.html").FullName);
        //}
    }
}
