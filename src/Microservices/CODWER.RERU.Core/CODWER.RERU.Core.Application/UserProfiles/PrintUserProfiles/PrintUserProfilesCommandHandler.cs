using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Core.DataTransferObjects.UserProfiles;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TableExportServices;
using CVU.ERP.ServiceProvider;
using MediatR;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.UserProfiles.PrintUserProfiles
{
    public  class PrintUserProfilesCommandHandler : IRequestHandler<PrintUserProfilesCommand, FileDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IExportData<UserProfile, UserProfileDto> _printer;
        private readonly ICurrentApplicationUserProvider _currentUserProvider;

        public PrintUserProfilesCommandHandler(AppDbContext appDbContext, IExportData<UserProfile, UserProfileDto> printer, ICurrentApplicationUserProvider currentUserProvider, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _printer = printer;
            _currentUserProvider = currentUserProvider;
            _mapper = mapper;
        }

        public async Task<FileDataDto> Handle(PrintUserProfilesCommand request, CancellationToken cancellationToken)
        {
            var filterData = new FilterUserProfilesDto
            {
                Keyword = request.Keyword,
                Email = request.Email,
                Idnp = request.Idnp,
                Status = request.Status,
                UserStatusEnum = request.UserStatusEnum,
                DepartmentId = request.DepartmentId,
                RoleId = request.RoleId
            };

            var currentUser = await _currentUserProvider.Get();
            var userProfile = _appDbContext.UserProfiles.FirstOrDefault(x => x.Id.ToString() == currentUser.Id);
            var userProfileDto = _mapper.Map<UserProfileDto>(userProfile);

            var userProfiles = GetAndFilterUserProfiles.Filter(_appDbContext, filterData, userProfileDto);

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
