using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Tests.GetMyEvaluatedTests.GetMyEvaluatedTestsByDate
{
    public class GetMyEvaluatedTestsByDateQueryHandler : IRequestHandler<GetMyEvaluatedTestsByDateQuery, PaginatedModel<TestDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;
        private readonly IUserProfileService _userProfileService;

        public GetMyEvaluatedTestsByDateQueryHandler(AppDbContext appDbContext, IPaginationService paginationService, IUserProfileService userProfileService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
            _userProfileService = userProfileService;
        }

        public async Task<PaginatedModel<TestDto>> Handle(GetMyEvaluatedTestsByDateQuery request, CancellationToken cancellationToken)
        {
            var currentUser = await _userProfileService.GetCurrentUser();

            var myTests = _appDbContext.Tests
                .Include(t => t.TestTemplate)
                .Include(t => t.UserProfile)
                .Include(t => t.Event)
                .Where(t => t.EvaluatorId == currentUser.Id ||
                            _appDbContext.EventEvaluators.Any(x => x.EventId == t.EventId && x.EvaluatorId == currentUser.Id))
                .Where(t => t.ProgrammedTime.Date == request.Date.Date)
                .AsQueryable();


            return await _paginationService.MapAndPaginateModelAsync<Test, TestDto>(myTests, request); ;
        }
    }
}
