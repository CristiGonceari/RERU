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

namespace CODWER.RERU.Evaluation.Application.EventResponsiblePersons.PrintEventResponsiblePersons
{
    public class PrintEventResponsiblePersonsCommandHandler : IRequestHandler<PrintEventResponsiblePersonsCommand, FileDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IExportData<UserProfile, UserProfileDto> _printer;

        public PrintEventResponsiblePersonsCommandHandler(IExportData<UserProfile, UserProfileDto> printer, AppDbContext appDbContext)
        {
            _printer = printer;
            _appDbContext = appDbContext;
        }

        public async Task<FileDataDto> Handle(PrintEventResponsiblePersonsCommand request, CancellationToken cancellationToken)
        {
            var responsiblePersons = _appDbContext.EventResponsiblePersons
                .Include(x => x.UserProfile)
                .Where(x => x.EventId == request.EventId)
                .Select(x => x.UserProfile)
                .OrderByFullName()
                .AsQueryable();

            var result = _printer.ExportTableSpecificFormat(new TableData<UserProfile>
            {
                Name = request.TableName,
                Items = responsiblePersons,
                Fields = request.Fields,
                Orientation = request.Orientation,
                ExportFormat = request.TableExportFormat
            });

            return result;
        }
    }
}
