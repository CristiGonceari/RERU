using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.SolicitedPositions.MySolicitedPositions.DeleteMySolicitedPosition
{
    public class DeleteMySolicitedPositionCommandHandler : IRequestHandler<DeleteMySolicitedPositionCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IUserProfileService _userProfileService;

        public DeleteMySolicitedPositionCommandHandler(AppDbContext appDbContext, IUserProfileService userProfileService)
        {
            _appDbContext = appDbContext;
            _userProfileService = userProfileService;
        }

        public async Task<Unit> Handle(DeleteMySolicitedPositionCommand request, CancellationToken cancellationToken)
        {
            var currentUserProfileId = await _userProfileService.GetCurrentUserId();

            var solicitedTest = await _appDbContext.SolicitedVacantPositions.FirstOrDefaultAsync(x => x.Id == request.Id);
            solicitedTest.UserProfileId = currentUserProfileId;

            _appDbContext.SolicitedVacantPositions.Remove(solicitedTest);

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
