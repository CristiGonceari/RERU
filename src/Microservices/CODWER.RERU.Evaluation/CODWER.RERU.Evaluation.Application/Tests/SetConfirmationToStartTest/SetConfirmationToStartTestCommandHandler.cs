using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Tests.SetConfirmationToStartTest
{
    public class SetConfirmationToStartTestCommandHandler : IRequestHandler<SetConfirmationToStartTestCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public SetConfirmationToStartTestCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(SetConfirmationToStartTestCommand request, CancellationToken cancellationToken)
        {
            var test = await _appDbContext.Tests.FirstAsync(x => x.Id == request.TestId);

            if (test.TestPassStatus == TestPassStatusEnum.Forbidden)
            {
                test.TestPassStatus = TestPassStatusEnum.Allowed;
            }

            if (test.TestStatus == TestStatusEnum.Programmed)
            {
                test.TestStatus = TestStatusEnum.AlowedToStart;
            }

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
