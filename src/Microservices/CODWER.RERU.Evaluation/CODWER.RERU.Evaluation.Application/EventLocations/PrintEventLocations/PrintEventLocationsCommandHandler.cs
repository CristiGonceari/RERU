using CODWER.RERU.Evaluation.DataTransferObjects.Locations;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.EventLocations.PrintEventLocations
{
    public class PrintEventLocationsCommandHandler : IRequestHandler<PrintEventLocationsCommand, FileDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IExportData<Location, LocationDto> _printer;

        public PrintEventLocationsCommandHandler(AppDbContext appDbContext, IExportData<Location, LocationDto> printer)
        {
            _appDbContext = appDbContext;
            _printer = printer;
        }

        public async Task<FileDataDto> Handle(PrintEventLocationsCommand request, CancellationToken cancellationToken)
        {
            var eventLocations = _appDbContext.EventLocations
                .Include(x => x.Location)
                .Where(x => x.EventId == request.EventId)
                .Select(x => x.Location)
                .AsQueryable();

            var result = _printer.ExportTableSpecificFormat(new TableData<Location>
            {
                Name = request.TableName,
                Items = eventLocations,
                Fields = request.Fields,
                Orientation = request.Orientation,
                ExportFormat = request.TableExportFormat
            });

            return result;
        }
    }
}
