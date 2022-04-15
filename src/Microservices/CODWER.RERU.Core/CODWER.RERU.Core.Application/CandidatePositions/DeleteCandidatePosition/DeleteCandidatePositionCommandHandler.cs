using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.CandidatePositions.DeleteCandidatePosition
{
    internal class DeleteCandidatePositionCommandHandler : IRequestHandler<DeleteCandidatePositionCommand, Unit>
    {
        private readonly AppDbContext _coreDbContext;
        private readonly ILoggerService<DeleteCandidatePositionCommand> _loggerService;

        public DeleteCandidatePositionCommandHandler(AppDbContext coreDbContext, ILoggerService<DeleteCandidatePositionCommand> loggerService)
        {
            _coreDbContext = coreDbContext;
            _loggerService = loggerService;
        }

        public async Task<Unit> Handle(DeleteCandidatePositionCommand request, CancellationToken cancellationToken)
        {
            var positionToDelete = await _coreDbContext.CandidatePositions.FirstAsync(x => x.Id == request.Id);

            _coreDbContext.CandidatePositions.Remove(positionToDelete);

            await _coreDbContext.SaveChangesAsync();

            await LogAction(positionToDelete);

            return Unit.Value;
        }

        private async Task LogAction(CandidatePosition candidatePosition)
        {
            await _loggerService.Log(LogData.AsCore($"Cadidate position {candidatePosition.Name} was deleted", candidatePosition));
        }
    }
}
