using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using CVU.ERP.Notifications.Email;
using CVU.ERP.Notifications.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Entities.StaticExtensions;
using RERU.Data.Persistence.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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

            var solicitedVacantPosition = await GetSolicitedVacantPosition(request);

            var events = await GetEventsWithTestTemplateList(solicitedVacantPosition);

            switch (request.Result)
            {
                case SolicitedVacantPositionEmailMessageEnum.Approve:
                    await AttachUserToEvents(events, solicitedVacantPosition.UserProfileId, solicitedVacantPosition.CandidatePosition.Id);
                    solicitedVacantPosition.SolicitedPositionStatus = SolicitedPositionStatusEnum.Approved;
                    break;
                case SolicitedVacantPositionEmailMessageEnum.Waiting:
                    solicitedVacantPosition.SolicitedPositionStatus = SolicitedPositionStatusEnum.Wait;
                    break;
                case SolicitedVacantPositionEmailMessageEnum.Reject:
                    solicitedVacantPosition.SolicitedPositionStatus = SolicitedPositionStatusEnum.Refused;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            await _appDbContext.SaveChangesAsync();

            await SendEmail(template, solicitedVacantPosition, request);

            return solicitedVacantPosition.Id;
        }

        private async Task AttachUserToEvents(List<EventsWithTestTemplateDto> events, int userProfileId, int positionId)
        {
            foreach (var eventDto in events)
            {
                if (await ExistEventUser(userProfileId, eventDto.Id)) continue;

                var newEventUser = new AddEventPersonDto
                {
                    UserProfileId = userProfileId,
                    EventId = eventDto.Id,
                    PositionId = positionId
                };

                var result = _mapper.Map<EventUser>(newEventUser);

                await _appDbContext.EventUsers.AddAsync(result);
                await _appDbContext.SaveChangesAsync();

                await AddEmailEventUserNotification(result);

            }
            await _appDbContext.SaveChangesAsync();
        }

        private async Task SendEmail(string template, SolicitedVacantPosition solicitedPosition, SendSolicitedVacantPositionEmailMessagesCommand request)
        {
            await _notificationService.PutEmailInQueue(new QueuedEmailData
            {
                Subject = "Pozitie solicitata",
                To = solicitedPosition.UserProfile.Email,
                HtmlTemplateAddress = "Templates/Evaluation/EmailNotificationTemplate.html",
                ReplacedValues = new Dictionary<string, string>()
                {
                    { "{user_name}", solicitedPosition.UserProfile.FullName },
                    { "{email_message}", request.EmailMessage }
                }
            });
        }

        private async Task AddEmailEventUserNotification(EventUser eventUser)
        {
            var item = await _appDbContext.EventUsers
                .Include(x => x.UserProfile)
                .Include(x => x.Event)
                .FirstOrDefaultAsync(x => x.Id == eventUser.Id);

            await _notificationService.PutEmailInQueue(new QueuedEmailData
            {
                Subject = "Invitație la eveniment",
                To = item.UserProfile.Email,
                HtmlTemplateAddress = "Templates/Evaluation/EmailNotificationTemplate.html",
                ReplacedValues = new Dictionary<string, string>()
                {
                    { "{user_name}", item.UserProfile.FullName },
                    { "{email_message}", GetTableContent(item) }
                }
            });
        }

        private string GetTableContent(EventUser eventUser)
            => $@"Dl/Dna {eventUser.UserProfile.GetFullName()}, sunteți invitat/ă la evenimentul “{eventUser.Event.Name}”, 
                în rol de candidat. În scurt timp veți primi invitație la test.Acesta v-a fi desfășurat online.";

        private async Task<SolicitedVacantPosition> GetSolicitedVacantPosition(SendSolicitedVacantPositionEmailMessagesCommand request) => 
                 _appDbContext.SolicitedVacantPositions
                .Include(x => x.CandidatePosition)
                .Include(x => x.UserProfile)
                .First(x => x.Id == request.SolicitedVacantPositionId);
        
        private async Task<List<EventsWithTestTemplateDto>> GetEventsWithTestTemplateList(SolicitedVacantPosition solicitedVacantPosition) =>
            await _appDbContext.EventVacantPositions
                .Include(c => c.Event)
                .AsQueryable()
                .Where(x => x.CandidatePositionId == solicitedVacantPosition.CandidatePositionId)
                .Select(tt => _mapper.Map<EventsWithTestTemplateDto>(tt))
                .ToListAsync();

        private async Task<bool> ExistEventUser(int userProfileId, int eventId) => 
            await _appDbContext.EventUsers.AnyAsync(x =>
                x.EventId == eventId && x.UserProfileId == userProfileId);
    }
}
