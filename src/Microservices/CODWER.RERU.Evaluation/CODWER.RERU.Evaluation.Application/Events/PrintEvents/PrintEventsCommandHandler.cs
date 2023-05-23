using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Linq;

namespace CODWER.RERU.Evaluation.Application.Events.PrintEvents
{
    public class PrintEventsCommandHandler : IRequestHandler<PrintEventsCommand, FileDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IExportData<Event, EventDto> _printer;

        public PrintEventsCommandHandler(AppDbContext appDbContext, IExportData<Event, EventDto> printer)
        {
            _appDbContext = appDbContext;
            _printer = printer;
        }

        public async Task<FileDataDto> Handle(PrintEventsCommand request, CancellationToken cancellationToken)
        {
            var events = GetAndFilterEvents.Filter(_appDbContext, request.Name, request.LocationKeyword, request.FromDate, request.TillDate);
            
            if (request.Date != null)
            {
                events = events.Where(p => p.FromDate.Date <= request.Date && p.TillDate.Date >= request.Date);
            }
            else if (request.StartTime != null && request.EndTime != null)
            {
                events = events.Where(x => 
                                (x.FromDate >= request.StartTime && x.FromDate <= request.EndTime) || 
                                (x.FromDate <= request.StartTime && x.TillDate >= request.StartTime) || 
                                (x.FromDate <= request.EndTime && x.TillDate >= request.EndTime) 
                );
            }

            var result = _printer.ExportTableSpecificFormat(new TableData<Event>
            {
                Name = request.TableName,
                Items = events,
                Fields = request.Fields,
                Orientation = request.Orientation,
                ExportFormat = request.TableExportFormat
            });

            return result;
        }
    }
}
