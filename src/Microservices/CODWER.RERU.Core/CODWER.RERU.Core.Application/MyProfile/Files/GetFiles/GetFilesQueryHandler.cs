using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.DataTransferObjects.Files;
using CVU.ERP.Common.Pagination;
using CVU.ERP.StorageService;
using CVU.ERP.StorageService.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CVU.ERP.ServiceProvider;

namespace CODWER.RERU.Core.Application.MyProfile.Files.GetFiles
{
    public class GetFilesQueryHandler : BaseHandler, IRequestHandler<GetFilesQuery, PaginatedModel<GetFilesDto>>
    {
        private readonly IPaginationService _paginationService;
        private readonly IStorageFileService _storageFileService;
        private readonly ICurrentApplicationUserProvider _currentApplication;

        public GetFilesQueryHandler(ICommonServiceProvider commonServiceProvider,
            IPaginationService paginationService,
            IStorageFileService storageFileService, 
            ICurrentApplicationUserProvider currentApplication) : base(commonServiceProvider)
        {
            _paginationService = paginationService;
            _storageFileService = storageFileService;
            _currentApplication = currentApplication;
        }

        public async Task<PaginatedModel<GetFilesDto>> Handle(GetFilesQuery request, CancellationToken cancellationToken)
        {
            var currentUser = await _currentApplication.Get();

            var fileIdList = AppDbContext.UserFiles
                .Where(x => x.UserProfileId == int.Parse(currentUser.Id))
                .Select(x => x.FileId).ToList();

            var files = await _storageFileService.GetUserFiles(fileIdList);

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<File, GetFilesDto>(files, request);

            return paginatedModel;
        }
    }
}
