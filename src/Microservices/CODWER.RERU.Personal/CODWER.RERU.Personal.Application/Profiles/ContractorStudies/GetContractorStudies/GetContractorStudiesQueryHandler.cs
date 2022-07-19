using System.Threading;
using System.Threading.Tasks;
using CVU.ERP.Common.Pagination;
using MediatR;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CODWER.RERU.Personal.DataTransferObjects.Studies;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.Application.Services;
using RERU.Data.Entities;

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
            var userProfile = await _userProfileService.GetCurrentUserProfile();

            var items = _appDbContext.Studies
                .Include(x => x.StudyType)
                .Where(x => x.UserProfileId == userProfile.Id)
                .AsQueryable();

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<Study, StudyDataDto>(items, request);

            return paginatedModel;
        }
    }
}
