using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Tests.DeleteTest
{
      public class DeleteTestCommandHandler : IRequestHandler<DeleteTestCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public DeleteTestCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(DeleteTestCommand request, CancellationToken cancellationToken)
        {
            var test = await _appDbContext.Tests.FirstOrDefaultAsync(x => x.Id == request.Id);

            _appDbContext.Tests.Remove(test);

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
