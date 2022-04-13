using AutoMapper;
using CODWER.RERU.Core.Data.Entities;
using CODWER.RERU.Core.Data.Persistence.Context;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.CandidatePositions.EditCandidatePosition
{
    public class EditCandidatePositionCommandHandler : IRequestHandler<EditCandidatePositionCommand, Unit>
    {
        private readonly CoreDbContext _coreDbContext;
        private readonly IMapper _mapper;
        private readonly ILoggerService<EditCandidatePositionCommand> _loggerService;

        public EditCandidatePositionCommandHandler(CoreDbContext coreDbContext, IMapper mapper, ILoggerService<EditCandidatePositionCommand> loggerService)
        {
            _coreDbContext = coreDbContext;
            _mapper = mapper;
            _loggerService = loggerService;
        }
        public async Task<Unit> Handle(EditCandidatePositionCommand request, CancellationToken cancellationToken)
        {
            var positionToEdit = await _coreDbContext.CandidatePositions.FirstAsync(x => x.Id == request.Data.Id);

            _mapper.Map(request.Data, positionToEdit);
            await _coreDbContext.SaveChangesAsync();

            await LogAction(positionToEdit);

            return Unit.Value;
        }

        private async Task LogAction(CandidatePosition candidatePosition)
        {
            await _loggerService.Log(LogData.AsCore($"Cadidate position {candidatePosition.Name} was edited", candidatePosition));
        }
    }
}
