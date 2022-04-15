using CODWER.RERU.Evaluation.DataTransferObjects.Locations;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TableExportServices;
using CVU.ERP.Module.Application.TableExportServices.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Locations.PrintLocations
{
    public class PrintLocationCommandHandler : IRequestHandler<PrintLocationCommand, FileDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IExportData<Location, LocationDto> _printer;

        public PrintLocationCommandHandler(AppDbContext appDbContext, IExportData<Location, LocationDto> printer)
        {
            _appDbContext = appDbContext;
            _printer = printer;
        }

        public async Task<FileDataDto> Handle(PrintLocationCommand request, CancellationToken cancellationToken)
        {
            var locations = GetAndFilterLocations.Filter(_appDbContext, request.Name, request.Address);

            var result = _printer.ExportTableSpecificFormat(new TableData<Location>
            {
                Name = request.TableName,
                Items = locations,
                Fields = request.Fields,
                Orientation = request.Orientation,
                ExportFormat = request.TableExportFormat
            });

            return result;
        }
    }
}
