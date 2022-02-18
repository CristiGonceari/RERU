using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.Application.Tests.GetMyTestsWithoutEvent;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.Tests.GetMyTestsWithoutEventByDate
{
    public class GetMyTestsWithoutEventByDateQueryHandler : IRequestHandler<GetMyTestsWithoutEventByDateQuery, PaginatedModel<TestDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IUserProfileService _userProfileService;
        private readonly IPaginationService _paginationService;

        public GetMyTestsWithoutEventByDateQueryHandler(AppDbContext appDbContext, IUserProfileService userProfileService, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
            _userProfileService = userProfileService;
        }

        public async Task<PaginatedModel<TestDto>> Handle(GetMyTestsWithoutEventByDateQuery request, CancellationToken cancellationToken)
        {
            var myUserProfile = await _userProfileService.GetCurrentUser();

            var myTests = _appDbContext.Tests
                .Include(t => t.TestTemplate)
                .Include(t => t.TestQuestions)
                .Include(t => t.UserProfile)
                .Include(t => t.Location)
                .Include(t => t.Event)
                .Where(t => t.UserProfileId == myUserProfile.Id && t.Event == null && t.ProgrammedTime.Date == request.Date.Date )
                .OrderByDescending(x => x.ProgrammedTime)
                .AsQueryable();


            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<Test, TestDto>(myTests, request);

            foreach (var myTest in paginatedModel.Items)
            {
                var testType = await _appDbContext.TestTemplates
                    .Include(tt => tt.Settings)
                    .FirstOrDefaultAsync(tt => tt.Id == myTest.TestTemplateId);

                if (testType.Settings.CanViewResultWithoutVerification)
                {
                    myTest.ViewTestResult = true;
                }
            }

            return paginatedModel;
        }
    }
}
