using CODWER.RERU.Logging.DataTransferObjects;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Logging.Context;
using CVU.ERP.Logging.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Logging.Application.GetLoggingValuesQuery
{
    public class GetLoggingValuesQueryHandler : IRequestHandler<GetLoggingValuesQuery, PaginatedModel<LogDto>>
    {
        private readonly LoggingDbContext _appDbContext;
        private readonly IPaginationService _paginationService;

        public GetLoggingValuesQueryHandler(LoggingDbContext appDbContext, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<LogDto>> Handle(GetLoggingValuesQuery request, CancellationToken cancellationToken)
        {
            var logs = _appDbContext.Logs.AsQueryable();

            logs = Filter(logs, request);

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<Log, LogDto>(logs, request);

            return paginatedModel;
        }

        private IQueryable<Log> Filter(IQueryable<Log> items, GetLoggingValuesQuery request)
        {
            if (!string.IsNullOrEmpty(request.Event))
            {
                items = items.Where(x => x.Event.Contains(request.Event));
            }

            if (!string.IsNullOrEmpty(request.ProjectName))
            {
                items = items.Where(x => x.Project.Contains(request.ProjectName));
            }

            if (!string.IsNullOrEmpty(request.UserName))
            {
                items = items.Where(x => x.UserName.Contains(request.UserName));
            }

            if (!string.IsNullOrEmpty(request.UserIdentifier))
            {
                items = items.Where(x => x.UserIdentifier.Contains(request.UserIdentifier));
            }

            if (request.FromDate != null)
            {
                items = items.Where(x => x.Date >= request.FromDate);
            }

            if (request.ToDate != null)
            {
                items = items.Where(x => x.Date <= request.ToDate);
            }

            return items;
        }
    }
}
