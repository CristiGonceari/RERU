using CODWER.RERU.Evaluation.DataTransferObjects.UserProfiles;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using RERU.Data.Persistence.Extensions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.EventEvaluators.PrintEventEvaluators
{
    public class PrintEventEvaluatorsCommandHandler : IRequestHandler<PrintEventEvaluatorsCommand, FileDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IExportData<UserProfile, UserProfileDto> _printer;

        public PrintEventEvaluatorsCommandHandler(IExportData<UserProfile, UserProfileDto> printer, AppDbContext appDbContext)
        {
            _printer = printer;
            _appDbContext = appDbContext;
        }

        public async Task<FileDataDto> Handle(PrintEventEvaluatorsCommand request, CancellationToken cancellationToken)
        {
            var eventEvaluators = _appDbContext.EventEvaluators
                .Include(x => x.Evaluator)
                .Where(x => x.EventId == request.EventId)
                .Select(x => x.Evaluator)
                .OrderByFullName()
                .AsQueryable();

            var result = _printer.ExportTableSpecificFormat(new TableData<UserProfile>
            {
                Name = request.TableName,
                Items = eventEvaluators,
                Fields = request.Fields,
                Orientation = request.Orientation,
                ExportFormat = request.TableExportFormat
            });

            return result;
        }
    }
}
