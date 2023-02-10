using AutoMapper;
using CODWER.RERU.Core.Data.Persistence.Helpers;
using CODWER.RERU.Core.DataTransferObjects.Profile;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TableExportServices;
using CVU.ERP.ServiceProvider;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.UserProfiles.PrintUserProfileModules
{
    public class PrintUserProfileModulesCommandHandler : IRequestHandler<PrintUserProfileModulesCommand, FileDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IExportData<UserProfileModuleRole, UserProfileModuleRowDto> _printer;
        private readonly ICurrentApplicationUserProvider _currentUserProvider;
        private readonly IMapper _mapper;

        public PrintUserProfileModulesCommandHandler(AppDbContext appDbContext, 
            IExportData<UserProfileModuleRole, UserProfileModuleRowDto> printer, 
            ICurrentApplicationUserProvider currentUserProvider, 
            IMapper mapper)
        {
            _appDbContext = appDbContext;
            _printer = printer;
            _currentUserProvider = currentUserProvider;
            _mapper = mapper;
        }

        public async Task<FileDataDto> Handle(PrintUserProfileModulesCommand request, CancellationToken cancellationToken)
        {
            var currentApplicationUser = await _currentUserProvider.Get();

            var userProfile = await _appDbContext.UserProfiles
                .IncludeBasic()
                .Include(x => x.Department)
                .Include(x => x.Role)
                .Include(x => x.ModuleRoles)
                .FirstOrDefaultAsync(up => up.Id == int.Parse(currentApplicationUser.Id));

            var userProfDto = _mapper.Map<UserProfileOverviewDto>(userProfile);

            var result = _printer.ExportTableSpecificFormatList(new TableListData<UserProfileModuleRowDto>
            {
                Name = request.TableName,
                Items = userProfDto.Modules.ToList(),
                Fields = request.Fields,
                Orientation = request.Orientation,
                ExportFormat = request.TableExportFormat
            });

            return result;
        }
    }
}
