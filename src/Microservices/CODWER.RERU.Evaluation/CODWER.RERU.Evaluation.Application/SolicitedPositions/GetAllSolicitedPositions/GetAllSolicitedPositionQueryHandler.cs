using MediatR;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.SolicitedPositions.GetAllSolicitedPositions
{
    public  class GetAllSolicitedPositionQueryHandler : IRequestHandler<GetAllSolicitedPositionQuery, List<SolicitedVacantPosition>>
    {
        private readonly AppDbContext _appDbContext;
        public GetAllSolicitedPositionQueryHandler(AppDbContext appDbContext) 
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<SolicitedVacantPosition>> Handle(GetAllSolicitedPositionQuery request, CancellationToken cancellationToken)
        {
            var values = _appDbContext.SolicitedVacantPositions.ToList();

            return values;
        }
    }
}
