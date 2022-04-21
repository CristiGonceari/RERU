using CODWER.RERU.Core.DataTransferObjects.UserProfiles;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.UserProfiles.PrintUserProfiles
{
    public  class PrintUserProfilesCommandHandler : IRequestHandler<PrintUserProfilesCommand, FileDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IExportData<UserProfile, UserProfileDto> _printer;

        public PrintUserProfilesCommandHandler(AppDbContext appDbContext, IExportData<UserProfile, UserProfileDto> printer)
        {
            _appDbContext = appDbContext;
            _printer = printer;
        }

        public async Task<FileDataDto> Handle(PrintUserProfilesCommand request, CancellationToken cancellationToken)
        {

            var filterData = new FilterUserProfilesDto
            {
                Keyword = request.Keyword,
                Email = request.Email,
                Idnp = request.Idnp
            };
            
            var userProfiles = GetAndFilterUserProfiles.Filter(_appDbContext, filterData);

            var result = _printer.ExportTableSpecificFormat(new TableData<UserProfile>
            {
                Name = request.TableName,
                Items = userProfiles,
                Fields = request.Fields,
                Orientation = request.Orientation,
                ExportFormat = request.TableExportFormat
            });

            return result;

        }
    }
}
