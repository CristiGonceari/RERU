using System;
using System.Collections.Generic;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CVU.ERP.Notifications.Email;
using CVU.ERP.Notifications.Services;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Tests.AddTest
{
     public class AddTestCommandHandler : IRequestHandler<AddTestCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;

        public AddTestCommandHandler(AppDbContext appDbContext, IMapper mapper, INotificationService notificationService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _notificationService = notificationService;
        }

        public async Task<int> Handle(AddTestCommand request, CancellationToken cancellationToken)
        {
            var newTest = _mapper.Map<Test>(request.Data);

            var eventDatas = _appDbContext.Events.FirstOrDefault(e => e.Id == newTest.EventId);

            if (eventDatas != null)
            {
                newTest.ProgrammedTime = eventDatas.FromDate;
                newTest.EndProgrammedTime = eventDatas.TillDate;
            }

            newTest.TestStatus = TestStatusEnum.Programmed;

            if (request.Data.EventId.HasValue)
            {
                var eventtestTemplate = await _appDbContext.EventTestTemplates.FirstOrDefaultAsync(x => x.EventId == request.Data.EventId.Value && x.TestTemplateId == request.Data.TestTemplateId);

                if (eventtestTemplate?.MaxAttempts != null)
                {
                    var attempts = _appDbContext.Tests.Count(x => x.UserProfileId == request.Data.UserProfileId 
                                                                  && x.EventId == request.Data.EventId.Value 
                                                                  && x.TestTemplateId == request.Data.TestTemplateId);

                    if (attempts >= eventtestTemplate?.MaxAttempts)
                    {
                        newTest.TestPassStatus = TestPassStatusEnum.Forbidden;
                    }
                }   
            }

            _appDbContext.Tests.Add(newTest);
            await _appDbContext.SaveChangesAsync();

            if (request.Data.LocationId.HasValue || request.Data.SolicitedTime.HasValue)
            {
                await SendEmailNotification(newTest.Id, request.Data.LocationId ?? 0);
            }

            return newTest.Id;
        }

        private async Task<Unit> SendEmailNotification(int testId, int locationId)
        {
            var test = await _appDbContext.Tests
                .Include(x => x.Event)
                    .ThenInclude(x => x.EventLocations)
                        .ThenInclude(x => x.Location)
                .Include(x => x.UserProfile)
                .Include(x => x.TestTemplate)
                .FirstOrDefaultAsync(x => x.Id == testId);

            await _notificationService.PutEmailInQueue(new QueuedEmailData
            {
                Subject = "Invitație la evaluare",
                To = test.UserProfile.Email,
                HtmlTemplateAddress = "Templates/Evaluation/EmailNotificationTemplate.html",
                ReplacedValues = new Dictionary<string, string>()
                {
                    { "{user_name}", test.UserProfile.FullName },
                    { "{email_message}",  await GetEmailContent(test.TestTemplate.Name, locationId, test.SolicitedTime)}
                }
            });

            return Unit.Value;
        }

        private async Task<string> GetEmailContent(string testName, int locationId, DateTime? time)
        {
            var location = _appDbContext.Locations.FirstOrDefault(x => x.Id == locationId);

            var content = $@"<p>sunteți invitat/ă la evaluarea ""{testName}""</p>. 
                             <p> Data și ora: ""{time.Value.ToString("dd/MM/yyyy HH:mm")}"". </p> ";

            if (location != null)
            {
                content += $@"<p> Locatia: ""{location.Address}"", ""{location.Name}"" </p>";
                content += $@"<p> ""{location.Description}""</p>";
            }

            content += $@"<p> Prezența fizică este obligatorie. </p>";

            return content;
        }

    }
}
