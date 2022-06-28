using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Tests.SetTestResult
{
    public class SetTestResultCommandHandler : IRequestHandler<SetTestResultCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public SetTestResultCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(SetTestResultCommand request, CancellationToken cancellationToken)
        {
            var test = await _appDbContext.Tests
                .Include(x => x.TestTemplate)
                .FirstOrDefaultAsync(x => x.Id == request.TestId);

            test.ResultStatus = request.ResultStatus;
            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
