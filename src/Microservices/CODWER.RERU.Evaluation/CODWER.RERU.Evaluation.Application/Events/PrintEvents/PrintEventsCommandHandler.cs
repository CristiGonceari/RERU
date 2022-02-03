using System.Linq;
using CVU.ERP.Common.DataTransferObjects.Files;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using CVU.ERP.Module.Application.TablePrinterService;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.Events.PrintEvents
{
    public class PrintEventsCommandHandler : IRequestHandler<PrintEventsCommand, FileDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly ITablePrinter<Event, EventDto> _printer;

        public PrintEventsCommandHandler(AppDbContext appDbContext, ITablePrinter<Event, EventDto> printer)
        {
            _appDbContext = appDbContext;
            _printer = printer;
        }

        public async Task<FileDataDto> Handle(PrintEventsCommand request, CancellationToken cancellationToken)
        {
            var events = _appDbContext.Events
                .Include(x => x.EventLocations)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                events = events.Where(x => x.Name.Contains(request.Name));
            }

            if (!string.IsNullOrWhiteSpace(request.LocationKeyword))
            {
                events = events.Where(x => x.EventLocations.Any(l => l.Location.Name.Contains(request.LocationKeyword)) || x.EventLocations.Any(l => l.Location.Address.Contains(request.LocationKeyword)));
            }

            var result = _printer.PrintTable(new TableData<Event>
            {
                Name = request.TableName,
                Items = events,
                Fields = request.Fields,
                Orientation = request.Orientation
            });

            return result;
        }
    }
}
