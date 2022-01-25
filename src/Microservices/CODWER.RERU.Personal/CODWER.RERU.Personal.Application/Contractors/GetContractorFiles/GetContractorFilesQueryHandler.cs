using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Data.Entities.Files;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Files;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Personal.Application.Contractors.GetContractorFiles
{
    public class GetContractorFilesQueryHandler : IRequestHandler<GetContractorFilesQuery, PaginatedModel<FileNameDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;

        public GetContractorFilesQueryHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<FileNameDto>> Handle(GetContractorFilesQuery request, CancellationToken cancellationToken)
        {
            var files = _appDbContext.ByteFiles
                .Where(x => x.ContractorId == request.ContractorId);

            if (request.Type != null)
            {
                files = files.Where(x => x.Type == request.Type);
            }

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<ByteArrayFile, FileNameDto>(files, request);

            return paginatedModel;
        }
    }
}
