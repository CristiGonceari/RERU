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

namespace CODWER.RERU.Evaluation.Application.LocationResponsiblePersons.PrintLocationResponsiblePersons
{
    public class PrintLocationResponsiblePersonsCommandHandler : IRequestHandler<PrintLocationResponsiblePersonsCommand, FileDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IExportData<UserProfile, UserProfileDto> _printer;

        public PrintLocationResponsiblePersonsCommandHandler(AppDbContext appDbContext, IExportData<UserProfile, UserProfileDto> printer)
        {
            _appDbContext = appDbContext;
            _printer = printer;
        }

        public async Task<FileDataDto> Handle(PrintLocationResponsiblePersonsCommand request, CancellationToken cancellationToken)
        {
            var responsiblePersons = _appDbContext.LocationResponsiblePersons
                .Include(x => x.UserProfile)
                .Where(x => x.LocationId == request.LocationId)
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
