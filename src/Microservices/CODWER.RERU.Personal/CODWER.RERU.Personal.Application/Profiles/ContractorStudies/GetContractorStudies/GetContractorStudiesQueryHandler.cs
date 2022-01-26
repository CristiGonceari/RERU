using System.Threading;
using System.Threading.Tasks;
using CVU.ERP.Common.Pagination;
using MediatR;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CODWER.RERU.Personal.DataTransferObjects.Studies;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CODWER.RERU.Personal.Application.Services;
using CODWER.RERU.Personal.Data.Entities.Studies;

namespace CODWER.RERU.Personal.Application.Profiles.ContractorStudies.GetContractorStudies
{
    public class GetContractorStudiesQueryHandler : IRequestHandler<GetContractorStudiesQuery, PaginatedModel<StudyDataDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;
        private readonly IUserProfileService _userProfileService;

        public GetContractorStudiesQueryHandler(AppDbContext appDbContext, IPaginationService paginationService, IUserProfileService userProfileService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
            _userProfileService = userProfileService;
        }

        public async Task<PaginatedModel<StudyDataDto>> Handle(GetContractorStudiesQuery request, CancellationToken cancellationToken)
        {
            var contractorId = await _userProfileService.GetCurrentContractorId();

            var items = _appDbContext.Studies
                .Include(x => x.StudyType)
                .Where(x => x.ContractorId == contractorId)
                .AsQueryable();

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<Study, StudyDataDto>(items, request);

            return paginatedModel;
        }
    }
}
