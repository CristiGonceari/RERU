﻿using AutoMapper;
using CODWER.RERU.Evaluation.Application.Services;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.CandidatePositions.EditCandidatePosition
{
    public class EditCandidatePositionCommandHandler : IRequestHandler<EditCandidatePositionCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ILoggerService<EditCandidatePositionCommand> _loggerService;
        private readonly IAssignDocumentsAndEventsToPosition _assignDocumentsAndEventsToPosition;
        private readonly ICandidatePositionNotificationService _candidatePositionNotificationService;

        public EditCandidatePositionCommandHandler(AppDbContext appDbContext, 
            IMapper mapper, 
            ILoggerService<EditCandidatePositionCommand> loggerService, 
            IAssignDocumentsAndEventsToPosition assignDocumentsAndEventsToPosition, 
            ICandidatePositionNotificationService candidatePositionNotificationService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _loggerService = loggerService;
            _assignDocumentsAndEventsToPosition = assignDocumentsAndEventsToPosition;
            _candidatePositionNotificationService = candidatePositionNotificationService;
        }

        public async Task<Unit> Handle(EditCandidatePositionCommand request, CancellationToken cancellationToken)
        {
            var positionToEdit = await _appDbContext.CandidatePositions.FirstAsync(x => x.Id == request.Data.Id);
            request.Data.IsActive = positionToEdit.IsActive;

            _mapper.Map(request.Data, positionToEdit);
            await _appDbContext.SaveChangesAsync();

            await _candidatePositionNotificationService
                .AddCandidatePositionNotification(request.Data.UserProfileIds, positionToEdit.Id);

            await _assignDocumentsAndEventsToPosition.AssignEventToPosition(request.Data.EventIds, positionToEdit);

            await _assignDocumentsAndEventsToPosition
                .AssignRequiredDocumentsToPosition(request.Data.RequiredDocuments, positionToEdit);

            await LogAction(positionToEdit);

            return Unit.Value;
        }

        private async Task LogAction(CandidatePosition candidatePosition)
        {
            await _loggerService.Log(LogData.AsEvaluation($@"Pozția vacantă ""{candidatePosition.Name}"" a fost editată", candidatePosition));
        }
    }
}
