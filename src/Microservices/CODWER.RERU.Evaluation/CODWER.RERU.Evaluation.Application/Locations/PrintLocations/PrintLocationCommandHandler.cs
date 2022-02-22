using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.Locations;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TablePrinterService;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.Locations.PrintLocations
{
    public class PrintLocationCommandHandler : IRequestHandler<PrintLocationCommand, FileDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly ITablePrinter<Location, LocationDto> _printer;

        public PrintLocationCommandHandler(AppDbContext appDbContext, ITablePrinter<Location, LocationDto> printer)
        {
            _appDbContext = appDbContext;
            _printer = printer;
        }

        public async Task<FileDataDto> Handle(PrintLocationCommand request, CancellationToken cancellationToken)
        {
            var locations = GetAndFilterLocations.Filter(_appDbContext, request.Name, request.Address);

            var result = _printer.PrintTable(new TableData<Location>
            {
                Name = request.TableName,
                Items = locations,
                Fields = request.Fields,
                Orientation = request.Orientation
            });

            return result;
        }
    }
}
