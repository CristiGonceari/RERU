﻿using System;
using System.Collections.Generic;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CVU.ERP.Common.DataTransferObjects.Config;
using CVU.ERP.Notifications.Email;
using CVU.ERP.Notifications.Services;
using Microsoft.Extensions.Options;
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
        private readonly PlatformConfig _platformConfig;

        public AddTestCommandHandler(AppDbContext appDbContext, 
            IMapper mapper, 
            INotificationService notificationService, 
            IOptions<PlatformConfig> options)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _notificationService = notificationService;
            _platformConfig = options.Value;
        }

        public async Task<int> Handle(AddTestCommand request, CancellationToken cancellationToken)
        {
            var newTest = _mapper.Map<Test>(request.Data);

            var eventData = _appDbContext.Events.FirstOrDefault(e => e.Id == newTest.EventId);

            if (eventData != null)
            {
                newTest.ProgrammedTime = eventData.FromDate;
                newTest.EndProgrammedTime = eventData.TillDate;
            }

            newTest.TestStatus = TestStatusEnum.Programmed;

            if (request.Data.EventId.HasValue)
            {
                var eventTestTemplate = await _appDbContext.EventTestTemplates.FirstOrDefaultAsync(x => x.EventId == request.Data.EventId.Value && x.TestTemplateId == request.Data.TestTemplateId);

                if (eventTestTemplate?.MaxAttempts != null)
                {
                    var attempts = _appDbContext.Tests.Count(x => x.UserProfileId == request.Data.UserProfileId 
                                                                  && x.EventId == request.Data.EventId.Value 
                                                                  && x.TestTemplateId == request.Data.TestTemplateId);

                    if (attempts >= eventTestTemplate?.MaxAttempts)
                    {
                        newTest.TestPassStatus = TestPassStatusEnum.Forbidden;
                    }
                }   
            }

            _appDbContext.Tests.Add(newTest);
            await _appDbContext.SaveChangesAsync();

            await SendEmailNotification(newTest.Id, request);

            return newTest.Id;
        }

        private async Task<Unit> SendEmailNotification(int testId, AddTestCommand request)
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
                    { "{email_message}",  await GetEmailContent(test.TestTemplate.Name, request)}
                }
            });

            return Unit.Value;
        }

        private async Task<string> GetEmailContent(string testName, AddTestCommand request)
        {
            var content = $@"<p>sunteți invitat/ă la evaluarea ""{testName}""</p>. ";

            if (request.Data.SolicitedTime.HasValue)
            {
                content += $@"<p>Data și ora: ""{request.Data.SolicitedTime.Value.ToString("dd/MM/yyyy HH:mm")}"".</p>";
            }

            if (request.Data.LocationId.HasValue)
            {
                var location = _appDbContext.Locations.First(x => x.Id == request.Data.LocationId);
               
                content += $@"<p> Locatia: ""{location.Address}"", ""{location.Name}"" </p>";
                content += $@"<p> ""{location.Description}""</p>";

                content += $@"<p> Prezența fizică este obligatorie. </p>";
            }

            return content;
        }
    }
}
