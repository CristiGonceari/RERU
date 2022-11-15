using CODWER.RERU.Evaluation.DataTransferObjects.CandidatePositions;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.CandidatePositions.PrintCandidatePosition
{
    public class PrintCandidatePositionCommandHandler : IRequestHandler<PrintCandidatePositionCommand, FileDataDto>
    {

        private readonly AppDbContext _appDbContext;
        private readonly IExportData<CandidatePosition, CandidatePositionDto> _printer;

        public PrintCandidatePositionCommandHandler(AppDbContext appDbContext, IExportData<CandidatePosition, CandidatePositionDto> printer)
        {
            _appDbContext = appDbContext;
            _printer = printer;
        }

        public async Task<FileDataDto> Handle(PrintCandidatePositionCommand request, CancellationToken cancellationToken)
        {
            var filterData = new PositionFiltersDto()
            {
                Name = request.Name,
                ResponsiblePersonName = request.ResponsiblePersonName,
                MedicalColumn = request.MedicalColumn,
                ActiveFrom = request.ActiveFrom,
                ActiveTo = request.ActiveTo
            };

            var positions = GetAndPrintCandidatePosition.Filter(_appDbContext, filterData);

            var result = _printer.ExportTableSpecificFormat(new TableData<CandidatePosition>
            {
                Name = request.TableName,
                Items = positions,
                Fields = request.Fields,
                Orientation = request.Orientation,
                ExportFormat = request.TableExportFormat
            });

            return result;
        }
    }
}
