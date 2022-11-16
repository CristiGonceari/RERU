using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using CVU.ERP.Notifications.Email;
using CVU.ERP.Notifications.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;
using System;
using System.Collections.Generic;
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
        private readonly ILoggerService<SendSolicitedVacantPositionEmailMessagesCommand> _loggerService;

        public SendSolicitedVacantPositionEmailMessagesCommandHandler(AppDbContext appDbContext, 
            IMapper mapper, 
            INotificationService notificationService1, 
            ILoggerService<SendSolicitedVacantPositionEmailMessagesCommand> loggerService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _notificationService = notificationService1;
            _loggerService = loggerService;
        }

        public async Task<int> Handle(SendSolicitedVacantPositionEmailMessagesCommand request, CancellationToken cancellationToken)
        {
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

            await LogAction(solicitedVacantPosition);

            await SendEmail(solicitedVacantPosition, request);

            return solicitedVacantPosition.Id;
        }

        private async Task AttachUserToEvents(List<EventsWithTestTemplateDto> events, int userProfileId, int positionId)
        {
            foreach (var eventDto in events)
            {
                if (await ExistEventEvaluator(userProfileId, eventDto.Id)) continue;

                if (await ExistEventUser(userProfileId, eventDto.Id))
                {
                    var eventUser = await GetEventUser(userProfileId, eventDto.Id);

                    await AddEventUserCandidatePosition(eventUser.Id, positionId);

                    continue;
                }

                var newEventUser = new AddEventPersonDto
                {
                    UserProfileId = userProfileId,
                    EventId = eventDto.Id,
                    PositionId = positionId
                };

                var result = _mapper.Map<EventUser>(newEventUser);

                await _appDbContext.EventUsers.AddAsync(result);
                await _appDbContext.SaveChangesAsync();

                await AddEventUserCandidatePosition(result.Id, positionId);

                await AddEmailEventUserNotification(result);

            }
            await _appDbContext.SaveChangesAsync();
        }

        private async Task AddEventUserCandidatePosition(int userEventId, int positionId)
        {
            var eventUserCandidatePosition = new EventUserCandidatePosition
            {
                EventUserId = userEventId,
                CandidatePositionId = positionId
            };

            await _appDbContext.EventUserCandidatePositions.AddAsync(eventUserCandidatePosition);
        }

        private async Task SendEmail(SolicitedVacantPosition solicitedPosition, SendSolicitedVacantPositionEmailMessagesCommand request)
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

        private async Task LogAction(SolicitedVacantPosition solicitedVacantPosition)
        {
            await _loggerService.Log(LogData.AsEvaluation($"Poziția solicitată {solicitedVacantPosition.CandidatePosition.Name}, " +
                                                          $"la care a aplicat {solicitedVacantPosition.UserProfile.FullName}, " +
                                                          $"a primit un statut nou {await ParseSolicitedVacantPositionStatus(solicitedVacantPosition.SolicitedPositionStatus)}", solicitedVacantPosition));
        }

        private string GetTableContent(EventUser eventUser)
            => $@"sunteți invitat/ă la evenimentul “{eventUser.Event.Name}”, 
                în rol de candidat. În scurt timp veți primi invitație la test. Acesta va fi desfășurat online.";

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

        private async Task<bool> ExistEventEvaluator(int userProfileId, int eventId) =>
            await _appDbContext.EventEvaluators.AnyAsync(x =>
                x.EventId == eventId && x.EvaluatorId == userProfileId);

        private async Task<EventUser> GetEventUser(int userProfileId, int eventId) =>
            await _appDbContext.EventUsers.FirstAsync(x =>
                x.UserProfileId == userProfileId && x.EventId == eventId);

        private async Task<string> ParseSolicitedVacantPositionStatus(SolicitedPositionStatusEnum status) => 
            status switch
            {
                SolicitedPositionStatusEnum.Approved => "aprobat",
                SolicitedPositionStatusEnum.New => "nou",
                SolicitedPositionStatusEnum.Refused => "refuzat",
                SolicitedPositionStatusEnum.Wait => "în așteptare",
                _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
            };
    }
}
