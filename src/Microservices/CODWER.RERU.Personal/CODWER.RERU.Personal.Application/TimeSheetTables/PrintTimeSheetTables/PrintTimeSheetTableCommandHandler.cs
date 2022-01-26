using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Application.Services;
using CODWER.RERU.Personal.Application.TimeSheetTables.GetTimeSheetTableValues;
using CODWER.RERU.Personal.DataTransferObjects.Files;
using MediatR;


namespace CODWER.RERU.Personal.Application.TimeSheetTables.PrintTimeSheetTables
{
    public class PrintTimeSheetTableCommandHandler : IRequestHandler<PrintTimeSheetTableCommand, ExportTimeSheetDto>
    {
        private readonly IMediator _mediator;
        private readonly ITimeSheetTableService _timeSheetTableService;

        public PrintTimeSheetTableCommandHandler(IMediator mediator, ITimeSheetTableService timeSheetTableService)
        {
            _mediator = mediator;
            _timeSheetTableService = timeSheetTableService;
        }

        public async Task<ExportTimeSheetDto> Handle(PrintTimeSheetTableCommand request, CancellationToken cancellationToken)
        {
            var command = new GetTimeSheetTableValuesQuery
            {
                ContractorName = request.ContractorName,
                FromDate = request.FromDate,
                ToDate = request.ToDate,
                DepartmentId = request.DepartmentId,
                Page = request.Page,
                ItemsPerPage = request.ItemsPerPage
            };

            var result = await _mediator.Send(command);

            var exportFile = await _timeSheetTableService.PrintTimeSheetTableData(result, request.FromDate, request.ToDate);

            return exportFile;
        }
    }
}
