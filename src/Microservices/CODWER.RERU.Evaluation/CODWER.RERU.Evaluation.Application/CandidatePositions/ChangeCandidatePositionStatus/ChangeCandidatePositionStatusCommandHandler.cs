using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.CandidatePositions.ChangeCandidatePositionStatus
{
    public class ChangeCandidatePositionStatusCommandHandler : IRequestHandler<ChangeCandidatePositionStatusCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public ChangeCandidatePositionStatusCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(ChangeCandidatePositionStatusCommand request, CancellationToken cancellationToken)
        {
            var position = await _appDbContext.CandidatePositions.FirstOrDefaultAsync(x => x.Id == request.PositionId);

            position.IsActive = position.IsActive switch
            {
                true => false,
                false => true
            };

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
