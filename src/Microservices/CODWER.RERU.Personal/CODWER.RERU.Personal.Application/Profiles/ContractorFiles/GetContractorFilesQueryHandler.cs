using CODWER.RERU.Personal.Application.Services;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Files;
using CVU.ERP.Common.Pagination;
using CVU.ERP.StorageService;
using CVU.ERP.StorageService.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.Profiles.ContractorFiles
{
    public class GetContractorFilesQueryHandler : IRequestHandler<GetContractorFilesQuery, PaginatedModel<FileNameDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;
        private readonly IUserProfileService _userProfileService;
        private readonly IPersonalStorageClient _personalStorageClient;
        public GetContractorFilesQueryHandler(AppDbContext appDbContext, 
            IPaginationService paginationService, 
            IUserProfileService userProfileService, 
            IPersonalStorageClient personalStorageClient)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
            _userProfileService = userProfileService;
            _personalStorageClient = personalStorageClient;
        }

        public async Task<PaginatedModel<FileNameDto>> Handle(GetContractorFilesQuery request, CancellationToken cancellationToken)
        {
            var contractorId = await _userProfileService.GetCurrentContractorId();

            var idList = _appDbContext.ContractorFiles
                .Where(x=>x.ContractorId == contractorId)
                .Select(x => x.FileId)
                .ToList();

            var files = await _personalStorageClient.GetContractorFiles(idList);

            if (request.FileType != null)
            {
                files = files.Where(x => x.FileType == request.FileType);
            }

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<File, FileNameDto>(files, request);

            return paginatedModel;
        }
    }
}
