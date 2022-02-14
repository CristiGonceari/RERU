using CODWER.RERU.Personal.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Files;
using CVU.ERP.Common.Pagination;
using CVU.ERP.StorageService;
using CVU.ERP.StorageService.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.Contractors.GetContractorFiles
{
    public class GetContractorFilesQueryHandler : IRequestHandler<GetContractorFilesQuery, PaginatedModel<FileNameDto>>
    {
        private readonly IPaginationService _paginationService;
        private readonly IPersonalStorageClient _personalStorageClient;
        private readonly AppDbContext _appDbContext;

        public GetContractorFilesQueryHandler(IPaginationService paginationService, IPersonalStorageClient personalStorageClient, AppDbContext appDbContext)
        {
            _paginationService = paginationService;
            _personalStorageClient = personalStorageClient;
            _appDbContext = appDbContext;
        }

        public async Task<PaginatedModel<FileNameDto>> Handle(GetContractorFilesQuery request, CancellationToken cancellationToken)
        {
            var fileIdList = _appDbContext.ContractorFiles
                .Where(x => x.ContractorId == request.ContractorId)
                .Select(x => x.FileId).ToList();

            var files = await _personalStorageClient.GetContractorFiles(fileIdList);

            if (request.FileType != null)
            {
                files = files.Where(x => x.FileType == request.FileType);
            }

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<File, FileNameDto>(files, request);

            return paginatedModel;
        }
    }
}
