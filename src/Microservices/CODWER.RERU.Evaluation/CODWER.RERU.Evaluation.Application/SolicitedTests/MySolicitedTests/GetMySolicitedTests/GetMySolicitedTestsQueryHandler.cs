using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.DataTransferObjects.SolicitedTests;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.SolicitedTests.MySolicitedTests.GetMySolicitedTests
{
    public class GetMySolicitedTestsQueryHandler : IRequestHandler<GetMySolicitedTestsQuery, PaginatedModel<SolicitedTestDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IUserProfileService _userProfileService;
        private readonly IPaginationService _paginationService;

        public GetMySolicitedTestsQueryHandler(AppDbContext appDbContext, IUserProfileService userProfileService, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
            _userProfileService = userProfileService;
        }

        public async Task<PaginatedModel<SolicitedTestDto>> Handle(GetMySolicitedTestsQuery request, CancellationToken cancellationToken)
        {
            var myUserProfile = await _userProfileService.GetCurrentUser();

            var mySolicitedTests = _appDbContext.SolicitedVacantPositions
                .Include(t => t.TestTemplate)
                .Include(t => t.UserProfile)
                .Include(t => t.CandidatePosition)
                .Include(t => t.Event)
                .Where(t => t.UserProfileId == myUserProfile.Id)
                .OrderByDescending(x => x.Id)
                .AsQueryable();

            return await _paginationService.MapAndPaginateModelAsync<SolicitedVacantPosition, SolicitedTestDto>(mySolicitedTests, request);
        }
    }
}
