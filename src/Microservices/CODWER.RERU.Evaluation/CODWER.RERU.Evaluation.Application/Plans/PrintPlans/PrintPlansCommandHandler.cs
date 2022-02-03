using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.Plans;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TablePrinterService;
using MediatR;
using System.Linq;
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
            var plans = _appDbContext.Plans.AsQueryable();

            if (!string.IsNullOrEmpty(request.Name))
            {
                plans = plans.Where(x => x.Name.Contains(request.Name));
            }

            if (request.FromDate != null && request.TillDate != null)
            {
                plans = plans.Where(p => p.FromDate.Date >= request.FromDate && p.TillDate.Date <= request.TillDate ||
                                                    (request.FromDate <= p.FromDate.Date && p.FromDate.Date <= request.TillDate) && (request.FromDate <= p.TillDate.Date && p.TillDate.Date >= request.TillDate) ||
                                                    (request.FromDate >= p.FromDate.Date && p.FromDate.Date <= request.TillDate) && (request.FromDate <= p.TillDate.Date && p.TillDate.Date <= request.TillDate));
            }

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
