using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.Roles.RemoveRole
{
    public class RemoveRoleCommandHandler : IRequestHandler<RemoveRoleCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public RemoveRoleCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(RemoveRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _appDbContext.Roles.FirstAsync(x => x.Id == request.Id);

            _appDbContext.Roles.Remove(role);

            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
