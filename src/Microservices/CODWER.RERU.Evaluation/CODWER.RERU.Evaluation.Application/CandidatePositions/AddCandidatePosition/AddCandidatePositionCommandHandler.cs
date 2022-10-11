using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CODWER.RERU.Evaluation.Application.Services;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using MediatR;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.CandidatePositionNotifications.AddCandidatePositionNotification;
using CODWER.RERU.Evaluation.DataTransferObjects.CandidatePositionNotifications;

namespace CODWER.RERU.Evaluation.Application.CandidatePositions.AddCandidatePosition
{
    public class AddCandidatePositionCommandHandler : IRequestHandler<AddCandidatePositionCommand, int >
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ILoggerService<AddCandidatePositionCommand> _loggerService;
        private readonly IAssignDocumentsAndEventsToPosition _assignDocumentsAndEventsToPosition;
        private readonly IMediator _mediator;
        private readonly List<int> _addedUsersIds = new();
        private readonly ICandidatePositionNotificationService _candidatePositionNotificationService;

        public AddCandidatePositionCommandHandler(AppDbContext appDbContext, 
            IMapper mapper,
            ILoggerService<AddCandidatePositionCommand> loggerService, 
            IAssignDocumentsAndEventsToPosition assignDocumentsAndEventsToPosition, 
            IMediator mediator, 
            ICandidatePositionNotificationService candidatePositionNotificationService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _loggerService = loggerService;
            _assignDocumentsAndEventsToPosition = assignDocumentsAndEventsToPosition;
            _mediator = mediator;
            _candidatePositionNotificationService = candidatePositionNotificationService;
        }

        public async Task<int> Handle(AddCandidatePositionCommand request, CancellationToken cancellationToken)
        {
            var candidatePosition = _mapper.Map<CandidatePosition>(request.Data);

            await _appDbContext.CandidatePositions.AddAsync(candidatePosition);
            await _appDbContext.SaveChangesAsync();

            await _candidatePositionNotificationService
                .AddCandidatePositionNotification(request.Data.UserProfileIds, candidatePosition.Id);

            await _assignDocumentsAndEventsToPosition.AssignEventToPosition(request.Data.EventIds, candidatePosition);

            await _assignDocumentsAndEventsToPosition
                .AssignRequiredDocumentsToPosition(request.Data.RequiredDocuments, candidatePosition);

            await LogAction(candidatePosition);

            return candidatePosition.Id;
        }
        private async Task LogAction(CandidatePosition candidatePosition)
        {
            await _loggerService.Log(LogData.AsCore($"Cadidate position {candidatePosition.Name} was added to the list", candidatePosition));
        }
    }
}
