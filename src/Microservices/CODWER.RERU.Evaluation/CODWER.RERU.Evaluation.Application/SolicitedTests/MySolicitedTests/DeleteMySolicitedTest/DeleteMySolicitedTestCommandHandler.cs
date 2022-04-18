using CODWER.RERU.Evaluation.Application.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.SolicitedTests.MySolicitedTests.DeleteMySolicitedTest
{
    public class DeleteMySolicitedTestCommandHandler : IRequestHandler<DeleteMySolicitedTestCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IUserProfileService _userProfileService;

        public DeleteMySolicitedTestCommandHandler(AppDbContext appDbContext, IUserProfileService userProfileService)
        {
            _appDbContext = appDbContext;
            _userProfileService = userProfileService;
        }

        public async Task<Unit> Handle(DeleteMySolicitedTestCommand request, CancellationToken cancellationToken)
        {
            var myUserProfile = await _userProfileService.GetCurrentUser();

            var solicitedTest = await _appDbContext.SolicitedTests.FirstOrDefaultAsync(x => x.Id == request.Id);
            solicitedTest.UserProfileId = myUserProfile.Id;

            _appDbContext.SolicitedTests.Remove(solicitedTest);

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
