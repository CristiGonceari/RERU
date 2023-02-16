using AutoMapper;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.DataTransferObjects.SolicitedPositions;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using CVU.ERP.Notifications.Email;
using CVU.ERP.Notifications.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.SolicitedPositions.MySolicitedPositions.AddMySolicitedPosition
{
    public class AddMySolicitedPositionCommandHandler : IRequestHandler<AddMySolicitedPositionCommand, AddSolicitedCandidatePositionResponseDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IUserProfileService _userProfileService;
        private readonly ILoggerService<AddMySolicitedPositionCommandHandler> _loggerService;
        private readonly INotificationService _notificationService;

        public AddMySolicitedPositionCommandHandler(AppDbContext appDbContext, 
            IMapper mapper, IUserProfileService userProfileService, 
            ILoggerService<AddMySolicitedPositionCommandHandler> loggerService, 
            INotificationService notificationService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _userProfileService = userProfileService;
            _loggerService = loggerService;
            _notificationService = notificationService;
        }

        public async Task<AddSolicitedCandidatePositionResponseDto> Handle(AddMySolicitedPositionCommand request, CancellationToken cancellationToken)
        {
            var myUserProfile = await _userProfileService.GetCurrentUserProfileDto();

            var solicitedTest = _mapper.Map<SolicitedVacantPosition>(request.Data);
            solicitedTest.UserProfileId = myUserProfile.Id;
            solicitedTest.SolicitedPositionStatus = SolicitedPositionStatusEnum.New;

            await _appDbContext.SolicitedVacantPositions.AddAsync(solicitedTest);
            await _appDbContext.SaveChangesAsync();
            await LogAction(solicitedTest);

            var solicitedVacantPosition = new AddSolicitedCandidatePositionResponseDto
            {
                SolicitedVacantPositionId = solicitedTest.Id,
                UserProfileId = solicitedTest.UserProfileId
            };

            await EmailPositionResponsiblePerson(request, myUserProfile.FullName);

            return solicitedVacantPosition;
        }

        private async Task LogAction(SolicitedVacantPosition solicitedVacantPosition)
        {
            var item = await _appDbContext.SolicitedVacantPositions
                .Include(x => x.CandidatePosition)
                .FirstOrDefaultAsync(x => x.Id == solicitedVacantPosition.Id);

            await _loggerService.Log(LogData.AsEvaluation($@"Poziția vacantă ""{item.CandidatePosition.Name}"" a fost candidată", item));
        }

        private async Task EmailPositionResponsiblePerson(AddMySolicitedPositionCommand request, string candidateName)
        {
            var position = _appDbContext.CandidatePositions.First(x => x.Id == request.Data.CandidatePositionId);

            var usersToNotify = _appDbContext.CandidatePositionNotifications
                .Include(x => x.UserProfile)
                .Where(x => x.CandidatePosition.Id == position.Id)
                .Select(x => new UserProfile
                {
                    Email = x.UserProfile.Email,
                    FirstName = x.UserProfile.FirstName,
                    LastName = x.UserProfile.LastName,
                    FatherName = x.UserProfile.FatherName,
                })
                .ToList();

            foreach (var user in usersToNotify)
            {
              await SendEmailForCandidatePosition(user, candidateName, position?.Name);
            }
        }

        private async Task<Unit> SendEmailForCandidatePosition(UserProfile responsiblePerson, string candidateName, string positionName)
        {
            await _notificationService.PutEmailInQueue(new QueuedEmailData
            {
                Subject = "Poziția candidată",
                To = responsiblePerson.Email,
                HtmlTemplateAddress = "Templates/Evaluation/EmailNotificationTemplate.html",
                ReplacedValues = new Dictionary<string, string>()
                {
                    { "{user_name}", responsiblePerson.FullName },
                    { "{email_message}", await GetEmailContentForCandidatePosition(candidateName, positionName) }
                }
            });

            return Unit.Value;
        }

        private async Task<string> GetEmailContentForCandidatePosition(string candidateName, string positionName) =>
                               $@"<p style=""font-size: 22px; font-weight: 300;"">candidatul ""{candidateName}"", a candidat la pozitia ""{positionName}"".</p>";
    }
}
