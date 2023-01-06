using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CVU.ERP.Common;
using RERU.Data.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Positions.DismissFromPosition
{
    public class DismissFromPositionCommandHandler : IRequestHandler<DismissFromPositionCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IDateTime _dateTime;

        public DismissFromPositionCommandHandler(AppDbContext appDbContext, IDateTime dateTime)
        {
            _appDbContext = appDbContext;
            _dateTime = dateTime;
        }

        public async Task<Unit> Handle(DismissFromPositionCommand request, CancellationToken cancellationToken)
        {
            var now = _dateTime.Now;

            var currentPosition = await _appDbContext.Positions
                .Where(p => p.ContractorId == request.ContractorId)
                .FirstOrDefaultAsync(x =>
                    (x.FromDate == null && x.ToDate == null)
                || (x.ToDate == null && x.FromDate != null && x.FromDate < now)
                || (x.FromDate == null && x.ToDate != null && x.ToDate > now)
                || (x.FromDate != null && x.ToDate != null && x.FromDate < now && x.ToDate > now));

            if (currentPosition != null)
            {
                currentPosition.ToDate = now;
               
                await _appDbContext.SaveChangesAsync();
            }

            return Unit.Value;
        }
    }
}
