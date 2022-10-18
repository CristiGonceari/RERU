using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities.Enums;
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

            if (request.ResultStatus == TestResultStatusEnum.Recommended)
            {
                test.RecommendedFor = string.Join(",", request.RecommendedFor.ToList().Select(x => x.ToString()));
                test.NotRecommendedFor = string.Join(",", request.NotRecommendedFor.ToList().Select(x => x.ToString()));
            }

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
