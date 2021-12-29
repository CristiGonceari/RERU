using CVU.ERP.Logging.Context;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Logging.Application.GetSelectValues.GetProjectSelectValues
{
    public class GetProjectSelectValuesQueryHandler : IRequestHandler<GetProjectSelectValuesQuery, List<string>>
    {
        private readonly LoggingDbContext _appDbContext;

        public GetProjectSelectValuesQueryHandler(LoggingDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<string>> Handle(GetProjectSelectValuesQuery request, CancellationToken cancellationToken)
        {
            return _appDbContext.Logs.Select(x => x.Project).Distinct().ToList();
        }
    }
}
