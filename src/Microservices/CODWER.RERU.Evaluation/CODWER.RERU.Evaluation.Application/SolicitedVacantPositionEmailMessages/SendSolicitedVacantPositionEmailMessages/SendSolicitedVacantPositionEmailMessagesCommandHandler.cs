using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation.Application.EventUsers.AssignUserToEvent;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using CVU.ERP.Notifications.Email;
using CVU.ERP.Notifications.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.SolicitedVacantPositionEmailMessages.SendSolicitedVacantPositionEmailMessages
{
    public class SendSolicitedVacantPositionEmailMessagesCommandHandler : IRequestHandler<SendSolicitedVacantPositionEmailMessagesCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;
        private readonly IMediator _mediator;
        private string _path = new FileInfo("PdfTemplates/EmailNotificationTemplate.html").FullName;

        public SendSolicitedVacantPositionEmailMessagesCommandHandler(AppDbContext appDbContext, 
            IMapper mapper, 
            IMediator mediator, INotificationService notificationService1)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _mediator = mediator;
            _notificationService = notificationService1;
        }

        public async Task<int> Handle(SendSolicitedVacantPositionEmailMessagesCommand request, CancellationToken cancellationToken)
        {
            var template = await File.ReadAllTextAsync(_path);

            var solicitedTest = _appDbContext.SolicitedVacantPositions
                .Include(x => x.CandidatePosition)
                .Include(x => x.UserProfile)
                .First(x => x.Id == request.SolicitedVacantPositionId);

            var events = await _appDbContext.EventVacantPositions
                .Include(c => c.Event)
                .AsQueryable()
                .Where(x => x.CandidatePositionId == solicitedTest.CandidatePositionId)
                .Select(tt => _mapper.Map<EventsWithTestTemplateDto>(tt))
                .ToListAsync();

            switch (request.Result)
            {
                case SolicitedVacantPositionEmailMessageEnum.Approve:
                    await AttachUserToEvents(events, new List<int> {solicitedTest.UserProfileId});
                    solicitedTest.SolicitedPositionStatus = SolicitedPositionStatusEnum.Approved;
                    break;
                case SolicitedVacantPositionEmailMessageEnum.Waiting:
                    solicitedTest.SolicitedPositionStatus = SolicitedPositionStatusEnum.Wait;
                    break;
                case SolicitedVacantPositionEmailMessageEnum.Reject:
                    solicitedTest.SolicitedPositionStatus = SolicitedPositionStatusEnum.Refused;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            await _appDbContext.SaveChangesAsync();

            await SendEmail(template, solicitedTest, request);

            return solicitedTest.Id;
        }

        private async Task AttachUserToEvents(List<EventsWithTestTemplateDto> events, List<int> userProfileList)
        {
            foreach (var command in events.Select(userEvent => new AssignUserToEventCommand
            {
                EventId = userEvent.Id,
                UserProfileId = userProfileList
            }))
            {
                await _mediator.Send(command);
            }
        }

        private async Task SendEmail(string template, SolicitedVacantPosition solicitedPosition, SendSolicitedVacantPositionEmailMessagesCommand request)
        {
            //template = template.Replace("{user_name}", solicitedPosition.UserProfile.GetFullName());
            //template = template.Replace("{email_message}", request.EmailMessage);

            //var emailData = new EmailData()
            //{
            //    subject = "Invitație la test",
            //    body = template,
            //    from = "Do Not Reply",
            //    to = solicitedPosition.UserProfile.Email
            //};

            //await _notificationService.Notify(emailData, NotificationType.Both);

            await _notificationService.PutEmailInQueue(new QueuedEmailData
            {
                Subject = "Pozitie solicitata",
                To = solicitedPosition.UserProfile.Email,
                HtmlTemplateAddress = "PdfTemplates/EmailNotificationTemplate.html",
                ReplacedValues = new Dictionary<string, string>()
                {
                    { "{user_name}", solicitedPosition.UserProfile.FullName },
                    { "{email_message}", request.EmailMessage }
                }
            });
        }
    }
}
