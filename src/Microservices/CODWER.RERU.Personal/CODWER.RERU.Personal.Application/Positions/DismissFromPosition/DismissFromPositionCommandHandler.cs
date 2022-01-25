using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Data.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Positions.DismissFromPosition
{
    public class DismissFromPositionCommandHandler : IRequestHandler<DismissFromPositionCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;

        public DismissFromPositionCommandHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Unit> Handle(DismissFromPositionCommand request, CancellationToken cancellationToken)
        {
            var now = DateTime.Now;

            var currentPosition = await _appDbContext.Positions
                .Where(p => p.ContractorId == request.ContractorId)
                .FirstOrDefaultAsync(x =>
                    (x.FromDate == null && x.ToDate == null)
                || (x.ToDate == null && x.FromDate != null && x.FromDate < now)
                || (x.FromDate == null && x.ToDate != null && x.ToDate > now)
                || (x.FromDate != null && x.ToDate != null && x.FromDate < now && x.ToDate > now));

            if (currentPosition != null)
            {
                currentPosition.ToDate = DateTime.Now;
               
                await _appDbContext.SaveChangesAsync();
            }

            return Unit.Value;
        }
    }
}
