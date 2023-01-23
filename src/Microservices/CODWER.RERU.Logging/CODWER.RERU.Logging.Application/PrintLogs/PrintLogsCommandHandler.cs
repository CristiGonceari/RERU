using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Logging.Context;
using CVU.ERP.Logging.Entities;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Logging.DataTransferObjects;

namespace CODWER.RERU.Logging.Application.PrintLogs
{
    public class PrintLogsCommandHandler : IRequestHandler<PrintLogsCommand, FileDataDto>
    {
        private readonly LoggingDbContext _appDbContext;
        private readonly IExportData<Log, LogDto> _printer;

        public PrintLogsCommandHandler(LoggingDbContext appDbContext, IExportData<Log, LogDto> printer)
        {
            _appDbContext = appDbContext;
            _printer = printer;
        }

        public async Task<FileDataDto> Handle(PrintLogsCommand request, CancellationToken cancellationToken)
        {
            var logs = _appDbContext.Logs
                .OrderByDescending(l => l.Date)
                .AsQueryable();

            logs = Filter(logs, request);

            var result = _printer.ExportTableSpecificFormat(new TableData<Log>
            {
                Name = request.TableName,
                Items = logs,
                Fields = request.Fields,
                Orientation = request.Orientation,
                ExportFormat = request.TableExportFormat
            });

            return result;
        }

        private IQueryable<Log> Filter(IQueryable<Log> items, PrintLogsCommand request)
        {
            if (!string.IsNullOrEmpty(request.Event))
            {
                items = items.Where(x => x.Event.ToLower().Contains(request.Event.ToLower()));
            }

            if (!string.IsNullOrEmpty(request.ProjectName))
            {
                items = items.Where(x => x.Project.ToLower().Contains(request.ProjectName.ToLower()));
            }

            if (!string.IsNullOrEmpty(request.UserName))
            {
                items = items.Where(x => x.UserName.ToLower().Contains(request.UserName.ToLower()));
            }

            if (!string.IsNullOrEmpty(request.UserIdentifier))
            {
                items = items.Where(x => x.UserIdentifier.Contains(request.UserIdentifier));
            }

            if (!string.IsNullOrEmpty(request.EventMessage))
            {
                items = items.Where(x => x.EventMessage.ToLower().Contains(request.EventMessage.ToLower()));
            }

            if (!string.IsNullOrEmpty(request.JsonMessage))
            {
                items = items.Where(x => x.JsonMessage.Replace(" ", string.Empty).Contains(request.JsonMessage));
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
