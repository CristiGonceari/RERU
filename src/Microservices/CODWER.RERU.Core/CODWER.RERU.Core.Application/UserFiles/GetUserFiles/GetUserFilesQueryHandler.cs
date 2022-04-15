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

namespace CODWER.RERU.Core.Application.UserFiles.GetUserFiles
{
    public class GetUserFilesQueryHandler : BaseHandler, IRequestHandler<GetUserFilesQuery, PaginatedModel<GetFilesDto>>
    {
        private readonly IPaginationService _paginationService;
        private readonly IStorageFileService _storageFileService;

        public GetUserFilesQueryHandler(ICommonServiceProvider commonServiceProvider, 
             IPaginationService paginationService, 
             IStorageFileService storageFileService) : base(commonServiceProvider)
        {
            _paginationService = paginationService;
            _storageFileService = storageFileService;
        }

        public async Task<PaginatedModel<GetFilesDto>> Handle(GetUserFilesQuery request, CancellationToken cancellationToken)
        {
            var fileIdList = AppDbContext.UserFiles
                                    .Where(x => x.UserProfileId == request.UserId)
                                    .Select(x => x.FileId).ToList();

            var files = await _storageFileService.GetUserFiles(fileIdList);

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<File, GetFilesDto>(files, request);

            return paginatedModel;
        }
    }
}
