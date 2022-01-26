using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Application.Services;
using CODWER.RERU.Personal.Data.Entities.Files;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Files;
using CVU.ERP.Common.Pagination;
using MediatR;

namespace CODWER.RERU.Personal.Application.Profiles.ContractorFiles
{
    public class GetContractorFilesQueryHandler : IRequestHandler<GetContractorFilesQuery, PaginatedModel<FileNameDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;
        private readonly IUserProfileService _userProfileService;

        public GetContractorFilesQueryHandler(AppDbContext appDbContext, IPaginationService paginationService, IUserProfileService userProfileService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
            _userProfileService = userProfileService;
        }

        public async Task<PaginatedModel<FileNameDto>> Handle(GetContractorFilesQuery request, CancellationToken cancellationToken)
        {
            var contractorId = await _userProfileService.GetCurrentContractorId();

            var items = _appDbContext.ByteFiles
                .Where(x=>x.ContractorId == contractorId)
                .AsQueryable();

            if (request.Type != null)
            {
                items = items.Where(x => x.Type == request.Type);
            }

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<ByteArrayFile, FileNameDto>(items, request);

            return paginatedModel;
        }
    }
}
