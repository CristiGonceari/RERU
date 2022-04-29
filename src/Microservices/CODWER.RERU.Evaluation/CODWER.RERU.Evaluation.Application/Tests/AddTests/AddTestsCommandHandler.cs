using System.Collections.Generic;
using CODWER.RERU.Evaluation.Application.Tests.AddTest;
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using MediatR;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.TestQuestions.GenerateTestQuestions;
using CVU.ERP.Notifications.Email;
using CVU.ERP.Notifications.Enums;
using CVU.ERP.Notifications.Services;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Tests.AddTests
{
    public class AddTestsCommandHandler : IRequestHandler<AddTestsCommand, List<int>>
    {
        private readonly IMediator _mediator;
        private readonly AppDbContext _appDbContext;
        private readonly INotificationService _notificationService;

        public AddTestsCommandHandler(IMediator mediator, AppDbContext appDbContext, INotificationService notificationService)
        {
            _mediator = mediator;
            _appDbContext = appDbContext;
            _notificationService = notificationService;
        }

        public async Task<List<int>> Handle(AddTestsCommand request, CancellationToken cancellationToken)
        {
            var testId = 0;
            var testsIds = new List<int>();

            foreach (var testCommand in request.UserProfileId.Select(id => new AddTestCommand
            {
                Data = new AddEditTestDto
                {
                    UserProfileId = id,
                    EvaluatorId = request.EvaluatorId,
                    ShowUserName = request.ShowUserName,
                    TestTemplateId = request.TestTemplateId,
                    EventId = request.EventId,
                    TestStatus = request.TestStatus,
                    ProgrammedTime = request.ProgrammedTime
                }
            }))
            {
                testId = await _mediator.Send(testCommand);
                testsIds.Add(testId);

                var generateCommand = new GenerateTestQuestionsCommand
                {
                    TestId = testId
                };

                await _mediator.Send(generateCommand);

                await SendEmailNotification(testCommand, null, testId);
            }

            await SendEmailNotification(null, request, testId);

            return testsIds;
        }

        private async Task<Unit> SendEmailNotification(AddTestCommand testCommand, AddTestsCommand request, int testId)
        {
            var path = new FileInfo("PdfTemplates/EmailNotificationTemplate.html").FullName;
            var template = await File.ReadAllTextAsync(path);

            var user = new UserProfile();
            var test = await _appDbContext.Tests
                .Include(x => x.TestTemplate)
                .FirstOrDefaultAsync(x => x.Id == testId);

            if (testCommand != null)
            {
                user = await _appDbContext.UserProfiles.FirstOrDefaultAsync(x => x.Id == testCommand.Data.UserProfileId);
                template = template.Replace("{email_message}", await GetTableContent(test, true));
            }
            else
            {
                if (request.EvaluatorId != null)
                {
                    user = await _appDbContext.UserProfiles.FirstOrDefaultAsync(x => x.Id == request.EvaluatorId);
                    template = template.Replace("{email_message}", await GetTableContent(test, false));
                }
                else
                {
                    return Unit.Value;
                }
            }

            template = template.Replace("{user_name}", user.FirstName + " " + user.LastName);

            var emailData = new EmailData()
            {
                subject = "Invitație la test",
                body = template,
                from = "Do Not Reply",
                to = user.Email
            };

            await _notificationService.Notify(emailData, NotificationType.Both);
                
            return Unit.Value;
        }

        private async Task<string> GetTableContent(Test item, bool evaluat)
        {
            var content = string.Empty;

            if (evaluat)
            {
                content += $@"<p style=""font-size: 22px; font-weight: 300;"">Ați fost invitat la testul ""{item.TestTemplate.Name}"" în rol de candidat.</p>
                          <p style=""font-size: 22px; font-weight: 300;""> Testul va avea loc pe data: {item.ProgrammedTime.ToString("dd/MM/yyyy")}.</p> 
                          <p>Pentru a accesa testul programat pe Dvs, urmati pasii:</p>
                          <p>1. Logati- va pe pagina <a href=""http://reru-stage.codwer.com/"" target=""_blank"">http://reru-stage.codwer.com/</a></p>
                          <p>2.Click pe butonul ""Evaluare"" </p>
                          <p> 3.Click pe butonul ""Activitatile mele"" </p>
                          <p> 4.Din meniul din stanga alegeti optiunea ""Eveniment"", daca testul a fost programat cu eveniment </p>
                          <p> 5.Din meniul din stanga alegeti optiunea ""Teste"", daca testul a fost programat fara eveniment </p>
                          <p> 6.Pentru a incepe testul, click ""Incepe Testul"" din partea dreapta de jos a paginii </p>
                          <p> 7.Bifati acceptarea regulilor, selecteaza ""Accepta"", si click butonul ""Incepe"" </p>";
            }
            else
            {
                content += $@"<p style=""font-size: 22px; font-weight: 300;"">Ați fost invitat la testul ""{item.TestTemplate.Name}"" în rol de evaluator.</p>";
            }

            return content;
        }
    }
}
