using System.Collections.Generic;
using CODWER.RERU.Evaluation.Application.Models;
using CVU.ERP.Notifications.Email;
using CVU.ERP.Notifications.Enums;
using CVU.ERP.Notifications.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.Tests.AddTests.SendEmailNotification
{
    public class SendEmailNotificationCommandHandler : IRequestHandler<SendEmailNotificationCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly INotificationService _notificationService;
        private readonly IOptions<PlatformConfig> _options;
        private readonly PlatformConfig _platformConfig;
        public SendEmailNotificationCommandHandler(AppDbContext appDbContext, INotificationService notificationService, IOptions<PlatformConfig> options, PlatformConfig platformConfig)
        {
            _appDbContext = appDbContext;
            _notificationService = notificationService;
            _platformConfig = options.Value;
            _options = options;
          
        }

        public async Task<Unit> Handle(SendEmailNotificationCommand request, CancellationToken cancellationToken)
        {
            foreach (var testId in request.TestIds)
            {
                await SendEmailNotification(true, testId);
                await SendEmailNotification(false, testId);
            }

            return Unit.Value;
        }

        private async Task<Unit> SendEmailNotification(bool forEvaluat, int testId)
        {
            //var path = new FileInfo("PdfTemplates/EmailNotificationTemplate.html").FullName;
            //var template = await File.ReadAllTextAsync(path);

            var user = new UserProfile();
            var test = await _appDbContext.Tests
                .Include(x => x.TestTemplate)
                .FirstOrDefaultAsync(x => x.Id == testId);

            if (forEvaluat)
            {
                user = await _appDbContext.UserProfiles.FirstOrDefaultAsync(x => x.Id == test.UserProfileId);
                //template = template.Replace("{email_message}", await GetTableContent(test, true));
            }
            else
            {
                if (test.EvaluatorId != null)
                {
                    user = await _appDbContext.UserProfiles.FirstOrDefaultAsync(x => x.Id == test.EvaluatorId);
                    //template = template.Replace("{email_message}", await GetTableContent(test, false));
                }
                else
                {
                    return Unit.Value;
                }
            }

            //template = template.Replace("{user_name}", user.FirstName + " " + user.LastName);

            //var emailData = new EmailData()
            //{
            //    subject = "Invitație la test",
            //    body = template,
            //    from = "Do Not Reply",
            //    to = user.Email
            //};

            //await _notificationService.Notify(emailData, NotificationType.Both);

            await _notificationService.PutEmailInQueue(new QueuedEmailData
            {
                Subject = "Invitație la test",
                To = user.Email,
                HtmlTemplateAddress = "PdfTemplates/EmailNotificationTemplate.html",
                ReplacedValues = new Dictionary<string, string>()
                {
                    { "{user_name}", user.FullName },
                    { "{email_message}", await GetTableContent(test, forEvaluat) }
                }
            });

            return Unit.Value;
        }

        private async Task<string> GetTableContent(Test item, bool evaluat)
        {
            var content = string.Empty;

            if (evaluat)
            {
                if (item.EventId != null)
                {
                    content += $@"<p style=""font-size: 22px; font-weight: 300;"">Ați fost invitat la testul ""{item.TestTemplate.Name}"" în rol de candidat.</p>
                            <p style=""font-size: 22px; font-weight: 300;""> Testul va avea loc din data de : {item.ProgrammedTime.ToString("dd/MM/yyyy")}.</p> 
                            <p style=""font-size: 22px; font-weight: 300;""> Până pe data de: {item.EndProgrammedTime?.ToString("dd/MM/yyyy")}.</p> 
                            <p>Pentru a accesa testul programat pe Dvs, urmati pasii:</p>
                            <p>1. Logati- va pe pagina {_platformConfig.BaseUrl} </p>
                            <p>2.Click pe butonul ""Evaluare"" </p>
                            <p> 3.Click pe butonul ""Activitatile mele"" </p>
                            <p> 4.Din meniul din stanga alegeti optiunea ""Eveniment"", daca testul a fost programat cu eveniment </p>
                            <p> 5.Din meniul din stanga alegeti optiunea ""Teste"", daca testul a fost programat fara eveniment </p>
                            <p> 6.Pentru a incepe testul, click ""Incepe Testul"" din partea dreapta de jos a paginii </p>
                            <p> 7.Bifati acceptarea regulilor, selecteaza ""Accepta"", si click butonul ""Incepe"" </p>";
                }
                else 
                {
                    content += $@"<p style=""font-size: 22px; font-weight: 300;"">Ați fost invitat la testul ""{item.TestTemplate.Name}"" în rol de candidat.</p>
                            <p style=""font-size: 22px; font-weight: 300;""> Testul va avea loc pe data: {item.ProgrammedTime.ToString("dd/MM/yyyy")}.</p> 
                            <p>Pentru a accesa testul programat pe Dvs, urmati pasii:</p>
                            <p>1. Logati- va pe pagina {_platformConfig.BaseUrl} </p>
                            <p>2.Click pe butonul ""Evaluare"" </p>
                            <p> 3.Click pe butonul ""Activitatile mele"" </p>
                            <p> 4.Din meniul din stanga alegeti optiunea ""Eveniment"", daca testul a fost programat cu eveniment </p>
                            <p> 5.Din meniul din stanga alegeti optiunea ""Teste"", daca testul a fost programat fara eveniment </p>
                            <p> 6.Pentru a incepe testul, click ""Incepe Testul"" din partea dreapta de jos a paginii </p>
                            <p> 7.Bifati acceptarea regulilor, selecteaza ""Accepta"", si click butonul ""Incepe"" </p>";  
                }
            }
            else
            {
                content += $@"<p style=""font-size: 22px; font-weight: 300;"">Ați fost invitat la testul ""{item.TestTemplate.Name}"" în rol de evaluator.</p>";
            }

            return content;
        }
    }
}
