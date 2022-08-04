using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.DataTransferObjects.SolicitedPositions;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.SolicitedPositions.MySolicitedPositions.GetMySolicitedTests
{
    public class GetMySolicitedPositionQueryHandler : IRequestHandler<GetMySolicitedPositionQuery, PaginatedModel<SolicitedCandidatePositionDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IUserProfileService _userProfileService;
        private readonly IPaginationService _paginationService;

        public GetMySolicitedPositionQueryHandler(AppDbContext appDbContext, IUserProfileService userProfileService, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
            _userProfileService = userProfileService;
        }

        public async Task<PaginatedModel<SolicitedCandidatePositionDto>> Handle(GetMySolicitedPositionQuery request, CancellationToken cancellationToken)
        {
            var myUserProfile = await _userProfileService.GetCurrentUser();

            var mySolicitedTests = _appDbContext.SolicitedVacantPositions
                .Include(t => t.UserProfile)
                .Include(t => t.CandidatePosition)
                .Include(x => x.SolicitedVacantPositionUserFiles)
                .Include(x => x.CandidatePosition.RequiredDocumentPositions)
                .Where(t => t.UserProfileId == myUserProfile.Id)
                .OrderByDescending(x => x.Id)
                .AsQueryable();

            return await _paginationService.MapAndPaginateModelAsync<SolicitedVacantPosition, SolicitedCandidatePositionDto>(mySolicitedTests, request);
        }
    }
}
