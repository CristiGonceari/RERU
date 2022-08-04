using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.DataTransferObjects.SolicitedPositions;
using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using CVU.ERP.Notifications.Email;
using CVU.ERP.Notifications.Services;
using MediatR;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.SolicitedPositions.MySolicitedPositions.AddMySolicitedPosition
{
    public class AddMySolicitedPositionCommandHandler : IRequestHandler<AddMySolicitedPositionCommand, AddSolicitedCandidatePositionResponseDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IUserProfileService _userProfileService;
        private readonly ILoggerService<AddMySolicitedPositionCommandHandler> _loggerService;
        private readonly ICandidatePositionService _candidatePositionService;
        private readonly INotificationService _notificationService;


        public AddMySolicitedPositionCommandHandler(AppDbContext appDbContext, 
            IMapper mapper, IUserProfileService userProfileService, 
            ILoggerService<AddMySolicitedPositionCommandHandler> loggerService, 
            ICandidatePositionService candidatePositionService, 
            INotificationService notificationService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _userProfileService = userProfileService;
            _loggerService = loggerService;
            _candidatePositionService = candidatePositionService;
            _notificationService = notificationService;
        }

        public async Task<AddSolicitedCandidatePositionResponseDto> Handle(AddMySolicitedPositionCommand request, CancellationToken cancellationToken)
        {
            var myUserProfile = await _userProfileService.GetCurrentUser();

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

        private async Task LogAction(SolicitedVacantPosition item)
        {
            await _loggerService.Log(LogData.AsEvaluation($"Solicited test was created", item));
        }

        private async Task EmailPositionResponsiblePerson(AddMySolicitedPositionCommand request, string candidateName)
        {
            var position = _appDbContext.CandidatePositions.FirstOrDefault(x => x.Id == request.Data.CandidatePositionId);

            var responsiblePerson = _candidatePositionService.GetResponsiblePerson(int.Parse(position?.CreateById ?? "0"));

            await SendEmailForCandidatePosition(responsiblePerson, candidateName, position?.Name);
        }

        private async Task<Unit> SendEmailForCandidatePosition(UserProfile responsiblePerson, string candidateName, string positionName)
        {
            await _notificationService.PutEmailInQueue(new QueuedEmailData
            {
                Subject = "Pozitia candidata",
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
                               $@"<p style=""font-size: 22px; font-weight: 300;"">Candidatul ""{candidateName}""</p>
                            <p style=""font-size: 22px;font-weight: 300;"">A candidat la pozitia ""{positionName}"".</p>";
    }
}
