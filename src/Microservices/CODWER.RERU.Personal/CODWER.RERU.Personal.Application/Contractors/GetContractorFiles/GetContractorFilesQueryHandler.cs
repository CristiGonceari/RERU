using CODWER.RERU.Personal.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Files;
using CVU.ERP.Common.Pagination;
using CVU.ERP.StorageService.Context;
using CVU.ERP.StorageService.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.Contractors.GetContractorFiles
{
    public class GetContractorFilesQueryHandler : IRequestHandler<GetContractorFilesQuery, PaginatedModel<FileNameDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;
        private readonly StorageDbContext _storageDbContext;

        public GetContractorFilesQueryHandler(AppDbContext appDbContext, IPaginationService paginationService, StorageDbContext storageDbContext)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
            _storageDbContext = storageDbContext;
        }

        public async Task<PaginatedModel<FileNameDto>> Handle(GetContractorFilesQuery request, CancellationToken cancellationToken)
        {
            var contractorFiles = _appDbContext.ContractorFiles
                .Where(x => x.ContractorId == request.ContractorId);

            var files = _storageDbContext.Files
                .Where(x => x.FileType == FileTypeEnum.IdentityFiles &&
                            contractorFiles.Any(i => i.FileId == x.Id.ToString()))
                .AsQueryable();

            if (request.FileType != null)
            {
                files = files.Where(x => x.FileType == request.FileType);
            }

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<File, FileNameDto>(files, request);

            return paginatedModel;
        }
    }
}
