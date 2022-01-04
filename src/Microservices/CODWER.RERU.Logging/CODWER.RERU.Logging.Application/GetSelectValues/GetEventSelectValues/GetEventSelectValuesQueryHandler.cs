using CVU.ERP.Logging.Context;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Logging.Application.GetSelectValues.GetEventSelectValues
{
    public class GetEventSelectValuesQueryHandler : IRequestHandler<GetEventSelectValuesQuery, List<string>>
    {
        private readonly LoggingDbContext _appDbContext;

        public GetEventSelectValuesQueryHandler(LoggingDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<string>> Handle(GetEventSelectValuesQuery request, CancellationToken cancellationToken)
        {
           return  _appDbContext.Logs.Select(x => x.Event).Distinct().ToList();
        }
    }
}
