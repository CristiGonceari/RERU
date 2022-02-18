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

namespace CODWER.RERU.Evaluation.Application.Events.PrintUserEvents
{
    public class PrintUserEventsCommandHandler : IRequestHandler<PrintUserEventsCommand, FileDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly ITablePrinter<Event, EventDto> _printer;

        public PrintUserEventsCommandHandler(AppDbContext appDbContext, ITablePrinter<Event, EventDto> printer)
        {
            _appDbContext = appDbContext;
            _printer = printer;
        }

        public async Task<FileDataDto> Handle(PrintUserEventsCommand request, CancellationToken cancellationToken)
        {
            var userEvents = _appDbContext.Events
                .Include(x => x.EventUsers)
                .Include(x => x.EventTestTypes)
                .ThenInclude(x => x.TestTemplate)
                .Where(x => x.EventUsers.Any(e => e.UserProfileId == request.UserId) && x.EventTestTypes.Any(e => e.TestTemplate.Mode == request.TestTypeMode))
                .AsQueryable();

            var result = _printer.PrintTable(new TableData<Event>
            {
                Name = request.TableName,
                Items = userEvents,
                Fields = request.Fields,
                Orientation = request.Orientation
            });

            return result;
        }
    }
}
