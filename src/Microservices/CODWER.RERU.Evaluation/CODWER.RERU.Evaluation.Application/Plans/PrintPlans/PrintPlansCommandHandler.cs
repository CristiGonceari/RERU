using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.Plans;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TablePrinterService;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.Plans.PrintPlans
{
    public class PrintPlansCommandHandler : IRequestHandler<PrintPlansCommand, FileDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly ITablePrinter<Plan, PlanDto> _printer;

        public PrintPlansCommandHandler(AppDbContext appDbContext, ITablePrinter<Plan, PlanDto> printer)
        {
            _appDbContext = appDbContext;
            _printer = printer;
        }

        public async Task<FileDataDto> Handle(PrintPlansCommand request, CancellationToken cancellationToken)
        {
            var plans = GetAndFilterPlans.Filter(_appDbContext, request.Name, request.TillDate, request.FromDate);

            var result = _printer.PrintTable(new TableData<Plan>
            {
                Name = request.TableName,
                Items = plans,
                Fields = request.Fields,
                Orientation = request.Orientation
            });

            return result;
        }
    }
}
