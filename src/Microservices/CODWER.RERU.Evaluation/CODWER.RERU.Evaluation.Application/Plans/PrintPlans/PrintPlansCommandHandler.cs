using CODWER.RERU.Evaluation.DataTransferObjects.Plans;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Linq;

namespace CODWER.RERU.Evaluation.Application.Plans.PrintPlans
{
    public class PrintPlansCommandHandler : IRequestHandler<PrintPlansCommand, FileDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IExportData<Plan, PlanDto> _printer;

        public PrintPlansCommandHandler(AppDbContext appDbContext, IExportData<Plan, PlanDto> printer)
        {
            _appDbContext = appDbContext;
            _printer = printer;
        }

        public async Task<FileDataDto> Handle(PrintPlansCommand request, CancellationToken cancellationToken)
        {
            var plans = GetAndFilterPlans.Filter(_appDbContext, request.Name, request.FromDate, request.TillDate);

            if (request.Date != null)
            {
                plans = plans.Where(p => p.FromDate.Date <= request.Date && p.TillDate.Date >= request.Date);
            }
            else if (request.StartTime != null && request.EndTime != null)
            {
                plans = plans.Where(x => 
                                (x.FromDate >= request.StartTime && x.FromDate <= request.EndTime) || 
                                (x.FromDate <= request.StartTime && x.TillDate >= request.StartTime) || 
                                (x.FromDate <= request.EndTime && x.TillDate >= request.EndTime) 
                );
            }

            var result = _printer.ExportTableSpecificFormat(new TableData<Plan>
            {
                Name = request.TableName,
                Items = plans,
                Fields = request.Fields,
                Orientation = request.Orientation,
                ExportFormat = request.TableExportFormat
            });

            return result;
        }
    }
}
