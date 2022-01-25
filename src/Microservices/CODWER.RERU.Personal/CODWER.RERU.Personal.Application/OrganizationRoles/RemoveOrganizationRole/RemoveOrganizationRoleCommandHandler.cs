using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Data.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.OrganizationRoles.RemoveOrganizationRole
{
    public class RemoveOrganizationRoleCommandHandler : IRequestHandler<RemoveOrganizationRoleCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public RemoveOrganizationRoleCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(RemoveOrganizationRoleCommand request, CancellationToken cancellationToken)
        {
            var toRemove = await _appDbContext.OrganizationRoles.FirstAsync(x => x.Id == request.Id);

            _appDbContext.OrganizationRoles.Remove(toRemove);
            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
