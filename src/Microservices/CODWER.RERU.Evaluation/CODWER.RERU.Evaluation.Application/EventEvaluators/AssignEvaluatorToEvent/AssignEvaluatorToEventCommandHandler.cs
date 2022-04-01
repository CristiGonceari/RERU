﻿using System.IO;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CVU.ERP.Notifications.Email;
using CVU.ERP.Notifications.Enums;
using CVU.ERP.Notifications.Services;
using Microsoft.EntityFrameworkCore;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.Application.Validation;
using System.Collections.Generic;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;

namespace CODWER.RERU.Evaluation.Application.EventEvaluators.AssignEvaluatorToEvent
{
    public class AssignEvaluatorToEventCommandHandler : IRequestHandler<AssignEvaluatorToEventCommand, List<int>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;
        private readonly IInternalNotificationService _internalNotificationService;

        public AssignEvaluatorToEventCommandHandler(AppDbContext appDbContext,
            IMapper mapper,
            INotificationService notificationService,
            IInternalNotificationService internalNotificationService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _notificationService = notificationService;
            _internalNotificationService = internalNotificationService;
        }

        public async Task<List<int>> Handle(AssignEvaluatorToEventCommand request, CancellationToken cancellationToken)
        {

            var eventEvaluatorIds = new List<int>();

            foreach (var evaluatorId in request.EvaluatorId)
            {
                var eventEvaluator = new AddEventEvaluatorDto()
                {
                    EvaluatorId = evaluatorId,
                    EventId = request.EventId,
                    ShowUserName = request.ShowUserName
                };

                var result = _mapper.Map<EventEvaluator>(eventEvaluator);

                await _appDbContext.EventEvaluators.AddAsync(result);
                await _appDbContext.SaveChangesAsync();

                var eventName = await _appDbContext.EventEvaluators
                    .Include(x => x.Event)
                    .FirstAsync(x => x.EventId == eventEvaluator.EventId && x.EvaluatorId == eventEvaluator.EvaluatorId);

                eventEvaluatorIds.Add(eventName.Id);

                await _internalNotificationService.AddNotification(eventEvaluator.EvaluatorId, NotificationMessages.YouWereInvitedToEventAsEvaluator);

                await SendEmailNotification(result);
            }

            return eventEvaluatorIds;
        }

        private async Task<Unit> SendEmailNotification(EventEvaluator eventEvaluator)
        {
            var user = await _appDbContext.EventEvaluators
                .Include(eu => eu.Evaluator)
                .Include(eu => eu.Event)
                .FirstOrDefaultAsync(x => x.Id == eventEvaluator.Id);

            var path = new FileInfo("PdfTemplates/EmailNotificationTemplate.html").FullName;
            var template = await File.ReadAllTextAsync(path);

            template = template
                .Replace("{user_name}", user.Evaluator.FirstName + " " + user.Evaluator.LastName)
                .Replace("{email_message}", await GetTableContent(eventEvaluator.Event.Name));

            var emailData = new EmailData
            {
                subject = "Invitație la eveniment",
                body = template,
                from = "Do Not Reply",
                to = user.Evaluator.Email
            };

            await _notificationService.Notify(emailData, NotificationType.Both);

            return Unit.Value;
        }

        private async Task<string> GetTableContent(string eventName)
        {
            var content = $@"<p style=""font-size: 22px; font-weight: 300;"">Ați fost invitat la evenimentul ""{eventName}"" în rol de evaluator.</p>";

            return content;
        }
    }
}
