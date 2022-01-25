using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Data.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Instructions.RemoveInstruction
{
    public class RemoveInstructionCommandHandler : IRequestHandler<RemoveInstructionCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public RemoveInstructionCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(RemoveInstructionCommand request, CancellationToken cancellationToken)
        {
            var toRemove = await _appDbContext.Instructions.FirstAsync(x => x.Id == request.Id);

            _appDbContext.Instructions.Remove(toRemove);
            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
