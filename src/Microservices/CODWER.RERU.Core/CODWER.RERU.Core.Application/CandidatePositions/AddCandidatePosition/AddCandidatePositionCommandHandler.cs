using AutoMapper;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.CandidatePositions.AddCandidatePosition
{
    public class AddCandidatePositionCommandHandler : IRequestHandler<AddCandidatePositionCommand, int>
    {
        private readonly AppDbContext _coreDbContext;
        private readonly IMapper _mapper;
        private readonly ILoggerService<AddCandidatePositionCommand> _loggerService;

        public AddCandidatePositionCommandHandler(AppDbContext coreDbContext, IMapper mapper, ILoggerService<AddCandidatePositionCommand> loggerService)
        {
            _coreDbContext = coreDbContext;
            _mapper = mapper;
            _loggerService = loggerService;
        }

        public async Task<int> Handle(AddCandidatePositionCommand request, CancellationToken cancellationToken)
        {
            var candidatePosition = _mapper.Map<CandidatePosition>(request.Data);

            await _coreDbContext.CandidatePositions.AddAsync(candidatePosition);
            await _coreDbContext.SaveChangesAsync();

            await LogAction(candidatePosition);

            return candidatePosition.Id;
        }

        private async Task LogAction(CandidatePosition candidatePosition)
        {
            await _loggerService.Log(LogData.AsCore($"Cadidate position {candidatePosition.Name} was added to the list", candidatePosition));
        }
    }
}
