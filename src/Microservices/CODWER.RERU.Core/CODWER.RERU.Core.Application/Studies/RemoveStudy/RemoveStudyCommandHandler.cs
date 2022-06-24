using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.Studies.RemoveStudy
{
    public class RemoveStudyCommandHandler : IRequestHandler<RemoveStudyCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public RemoveStudyCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(RemoveStudyCommand request, CancellationToken cancellationToken)
        {
            var toRemove = await _appDbContext.Studies.FirstAsync(x => x.Id == request.Id);

            _appDbContext.Studies.Remove(toRemove);
            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
