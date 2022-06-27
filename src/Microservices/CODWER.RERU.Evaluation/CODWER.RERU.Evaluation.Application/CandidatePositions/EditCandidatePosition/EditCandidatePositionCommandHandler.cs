using AutoMapper;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.Services;

namespace CODWER.RERU.Evaluation.Application.CandidatePositions.EditCandidatePosition
{
    public class EditCandidatePositionCommandHandler : IRequestHandler<EditCandidatePositionCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ILoggerService<EditCandidatePositionCommand> _loggerService;
        private readonly IAssignDocumentsToPosition _assignDocumentsToPosition;

        public EditCandidatePositionCommandHandler(AppDbContext appDbContext, IMapper mapper, ILoggerService<EditCandidatePositionCommand> loggerService, IAssignDocumentsToPosition assignDocumentsToPosition)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _loggerService = loggerService;
            _assignDocumentsToPosition = assignDocumentsToPosition;
        }
        public async Task<Unit> Handle(EditCandidatePositionCommand request, CancellationToken cancellationToken)
        {
            var positionToEdit = await _appDbContext.CandidatePositions.FirstAsync(x => x.Id == request.Data.Id);

            _mapper.Map(request.Data, positionToEdit);
            await _appDbContext.SaveChangesAsync();

            await _assignDocumentsToPosition
                .AssignRequiredDocumentsToPosition(request.Data.RequiredDocuments, positionToEdit);

            await LogAction(positionToEdit);

            return Unit.Value;
        }

        private async Task LogAction(CandidatePosition candidatePosition)
        {
            await _loggerService.Log(LogData.AsCore($"Cadidate position {candidatePosition.Name} was edited", candidatePosition));
        }
    }
}
