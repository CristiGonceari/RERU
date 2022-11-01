using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Tests.MyActivities.GetMyEvaluatedTests
{
    public class GetMyEvaluatedTestsQueryHandler : IRequestHandler<GetMyEvaluatedTestsQuery, PaginatedModel<TestDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;
        private readonly IUserProfileService _userProfileService;

        public GetMyEvaluatedTestsQueryHandler(AppDbContext appDbContext, IPaginationService paginationService, IUserProfileService userProfileService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
            _userProfileService = userProfileService;
        }

        public async Task<PaginatedModel<TestDto>> Handle(GetMyEvaluatedTestsQuery request, CancellationToken cancellationToken)
        {
            var currentUserId = await _userProfileService.GetCurrentUserId();

            var myTests = _appDbContext.Tests
                .Include(t => t.TestTemplate)
                .Include(t => t.UserProfile)
                .Include(t => t.Event)
                .Where(t => (t.EvaluatorId == currentUserId ||
                             _appDbContext.EventEvaluators.Any(x =>
                                 x.EventId == t.EventId && x.EvaluatorId == currentUserId)) && t.TestTemplate.Mode == TestTemplateModeEnum.Test)
                .AsQueryable();

            if (request.StartTime != null && request.EndTime != null)
            {
                myTests = myTests.Where(p => p.StartTime >= request.StartTime && p.EndTime <= request.EndTime ||
                                             (request.StartTime <= p.StartTime && p.StartTime <= request.EndTime) && (request.StartTime <= p.EndTime && p.EndTime >= request.EndTime) ||
                                             (request.StartTime >= p.StartTime && p.StartTime <= request.EndTime) && (request.StartTime <= p.EndTime && p.EndTime <= request.EndTime));
            }

            return await _paginationService.MapAndPaginateModelAsync<Test, TestDto>(myTests, request); ;

        }
    }
}
