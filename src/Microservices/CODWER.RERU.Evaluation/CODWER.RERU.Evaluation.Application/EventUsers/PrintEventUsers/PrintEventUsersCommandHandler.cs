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

namespace CODWER.RERU.Evaluation.Application.EventUsers.PrintEventUsers
{
    public class PrintEventUsersCommandHandler : IRequestHandler<PrintEventUsersCommand, FileDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IExportData<UserProfile, UserProfileDto> _printer;

        public PrintEventUsersCommandHandler(IExportData<UserProfile, UserProfileDto> printer, AppDbContext appDbContext)
        {
            _printer = printer;
            _appDbContext = appDbContext;
        }

        public async Task<FileDataDto> Handle(PrintEventUsersCommand request, CancellationToken cancellationToken)
        {
            var eventUsers = _appDbContext.EventUsers
                .Include(x => x.UserProfile)
                .Where(x => x.EventId == request.EventId)
                .Select(x => x.UserProfile)
                .OrderByFullName()
                .AsQueryable();

            var result = _printer.ExportTableSpecificFormat(new TableData<UserProfile>
            {
                Name = request.TableName,
                Items = eventUsers,
                Fields = request.Fields,
                Orientation = request.Orientation,
                ExportFormat = request.TableExportFormat
            });

            return result;
        }
    }
}
