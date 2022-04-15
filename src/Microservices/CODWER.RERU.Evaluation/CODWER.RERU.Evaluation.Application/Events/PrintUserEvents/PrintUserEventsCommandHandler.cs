using CODWER.RERU.Evaluation.DataTransferObjects.Events;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TableExportServices;
using CVU.ERP.Module.Application.TableExportServices.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Events.PrintUserEvents
{
    public class PrintUserEventsCommandHandler : IRequestHandler<PrintUserEventsCommand, FileDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IExportData<Event, EventDto> _printer;

        public PrintUserEventsCommandHandler(AppDbContext appDbContext, IExportData<Event, EventDto> printer)
        {
            _appDbContext = appDbContext;
            _printer = printer;
        }

        public async Task<FileDataDto> Handle(PrintUserEventsCommand request, CancellationToken cancellationToken)
        {
            var userEvents = _appDbContext.Events
                .Include(x => x.EventUsers)
                .Include(x => x.EventTestTemplates)
                .ThenInclude(x => x.TestTemplate)
                .Where(x => x.EventUsers.Any(e => e.UserProfileId == request.UserId) && x.EventTestTemplates.Any(e => e.TestTemplate.Mode == request.TestTemplateMode))
                .AsQueryable();

            var result = _printer.ExportTableSpecificFormat(new TableData<Event>
            {
                Name = request.TableName,
                Items = userEvents,
                Fields = request.Fields,
                Orientation = request.Orientation,
                ExportFormat = request.TableExportFormat
            });

            return result;
        }
    }
}
