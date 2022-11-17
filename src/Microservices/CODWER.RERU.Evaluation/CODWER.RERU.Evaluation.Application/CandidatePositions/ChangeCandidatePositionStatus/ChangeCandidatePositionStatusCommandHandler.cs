using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.CandidatePositions.ChangeCandidatePositionStatus
{
    public class ChangeCandidatePositionStatusCommandHandler : IRequestHandler<ChangeCandidatePositionStatusCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILoggerService<ChangeCandidatePositionStatusCommand> _loggerService;


        public ChangeCandidatePositionStatusCommandHandler(AppDbContext appDbContext, ILoggerService<ChangeCandidatePositionStatusCommand> loggerService)
        {
            _appDbContext = appDbContext;
            _loggerService = loggerService;
        }

        public async Task<Unit> Handle(ChangeCandidatePositionStatusCommand request, CancellationToken cancellationToken)
        {
            var position = await _appDbContext.CandidatePositions.FirstOrDefaultAsync(x => x.Id == request.PositionId);

            position.IsActive = !position.IsActive;

            await _appDbContext.SaveChangesAsync();

            await LogAction(position);

            return Unit.Value;
        }

        private async Task LogAction(CandidatePosition candidatePosition)
        {
            await _loggerService.Log(LogData.AsEvaluation($"Pozția vacantă {candidatePosition.Name} a primit un statut nou {GetStatus(candidatePosition.IsActive)}", candidatePosition));
        }

        private string GetStatus(bool isActive) => isActive ? "activ" : "inactiv";

    }
}
