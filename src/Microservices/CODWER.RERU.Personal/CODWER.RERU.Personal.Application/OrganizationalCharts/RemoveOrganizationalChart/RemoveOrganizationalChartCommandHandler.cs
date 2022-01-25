using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Data.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.OrganizationalCharts.RemoveOrganizationalChart
{
    public class RemoveOrganizationalChartCommandHandler : IRequestHandler<RemoveOrganizationalChartCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public RemoveOrganizationalChartCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(RemoveOrganizationalChartCommand request, CancellationToken cancellationToken)
        {
            var toRemove = await _appDbContext.OrganizationalCharts.FirstAsync(x => x.Id == request.Id);

            _appDbContext.OrganizationalCharts.Remove(toRemove);
            await _appDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
