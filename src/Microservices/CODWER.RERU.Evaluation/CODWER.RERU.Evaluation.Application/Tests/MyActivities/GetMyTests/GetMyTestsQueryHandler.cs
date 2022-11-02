using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Tests.MyActivities.GetMyTests
{
    public class GetMyTestsQueryHandler : IRequestHandler<GetMyTestsQuery, PaginatedModel<TestDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IUserProfileService _userProfileService;
        private readonly IPaginationService _paginationService;

        public GetMyTestsQueryHandler(AppDbContext appDbContext, IUserProfileService userProfileService, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
            _userProfileService = userProfileService;
        }

        public async Task<PaginatedModel<TestDto>> Handle(GetMyTestsQuery request, CancellationToken cancellationToken)
        {
            var currentUserId = await _userProfileService.GetCurrentUserId();

            var myTests = _appDbContext.Tests
                .Include(t => t.TestTemplate)
                    .ThenInclude(tt => tt.Settings)
                .Include(t => t.TestQuestions)
                .Include(t => t.UserProfile)
                .Include(t => t.Location)
                .Include(t => t.Event)
                .Where(t => t.UserProfileId == currentUserId &&
                            t.TestTemplate.Mode == TestTemplateModeEnum.Test &&
                            (t.EventId != null
                                ? t.ProgrammedTime.Date <= request.Date && t.EndProgrammedTime.Value.Date >= request.Date
                                : t.ProgrammedTime.Date == request.Date.Date))
                .OrderByDescending(x => x.CreateDate)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.TestName))
            {
                myTests = myTests.Where(x => x.TestTemplate.Name.ToLower().Contains(request.TestName.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(request.EventName))
            {
                myTests = myTests.Where(x => x.Event.Name.ToLower().Contains(request.EventName.ToLower()));
            }

            return await _paginationService.MapAndPaginateModelAsync<Test, TestDto>(myTests, request);
        }
    }
}
