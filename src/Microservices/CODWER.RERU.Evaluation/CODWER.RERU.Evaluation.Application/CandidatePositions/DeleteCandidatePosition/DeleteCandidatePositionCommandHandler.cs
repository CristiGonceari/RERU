using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.CandidatePositions.DeleteCandidatePosition
{
    public class DeleteCandidatePositionCommandHandler : IRequestHandler<DeleteCandidatePositionCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILoggerService<DeleteCandidatePositionCommand> _loggerService;

        public DeleteCandidatePositionCommandHandler(AppDbContext appDbContext, ILoggerService<DeleteCandidatePositionCommand> loggerService)
        {
            _appDbContext = appDbContext;
            _loggerService = loggerService;
        }

        public async Task<Unit> Handle(DeleteCandidatePositionCommand request, CancellationToken cancellationToken)
        {
            var positionToDelete = await _appDbContext.CandidatePositions.FirstAsync(x => x.Id == request.Id);

            _appDbContext.CandidatePositions.Remove(positionToDelete);

            await _appDbContext.SaveChangesAsync();

            await LogAction(positionToDelete);

            return Unit.Value;
        }

        private async Task LogAction(CandidatePosition candidatePosition)
        {
            await _loggerService.Log(LogData.AsCore($@"Poziția vacantă ""{candidatePosition.Name}"" a fost ștearsă", candidatePosition));
        }
    }
}
