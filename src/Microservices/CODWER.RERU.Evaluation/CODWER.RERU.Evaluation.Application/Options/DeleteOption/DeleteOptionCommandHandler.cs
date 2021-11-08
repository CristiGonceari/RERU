using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Options.DeleteOption
{
    public class DeleteOptionCommandHandler : IRequestHandler<DeleteOptionCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public DeleteOptionCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(DeleteOptionCommand request, CancellationToken cancellationToken)
        {
            var option = await _appDbContext.Options.FirstOrDefaultAsync(x => x.Id == request.Id);

            _appDbContext.Options.Remove(option);

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
